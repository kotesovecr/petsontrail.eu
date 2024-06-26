using API.GRPCService.Extensions;
using API.GRPCService.Interceptors;
using API.GRPCService.Options;
using API.GRPCService.Services.ActionRights;
using API.GRPCService.Services.Actions;
using API.GRPCService.Services.Activities;
using API.GRPCService.Services.Checkpoints;
using API.GRPCService.Services.Entries;
using API.GRPCService.Services.LiveUpdatesSubscription;
using API.GRPCService.Services.Pets;
using API.GRPCService.Services.Results;
using API.GRPCService.Services.UserProfiles;
using PetsOnTrail.Api;
using Google.Protobuf.Collections;
using Import;
using Mapster;
using MapsterMapper;
using Storage;
using Storage.Options;
using System.Security.Cryptography.X509Certificates;
using Protos.Actions;

var builder = WebApplication.CreateBuilder(args);

try 
{ 
    var certPem = File.ReadAllText("/app/fullchain.pem");
    var keyPem = File.ReadAllText("/app/privkey.pem");
    var x509 = X509Certificate2.CreateFromPem(certPem, keyPem);

    builder.WebHost.ConfigureKestrel(s => {
        s.ListenAnyIP(443, options =>
        {
            options.UseHttps(x509);
        });
    });
}
catch (Exception ex)
{
    try
    {
        var files = Directory.EnumerateFiles("/app/", "*", SearchOption.AllDirectories);

        foreach (string currentFile in files)
        {
            Console.WriteLine(currentFile);
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}


string MongoDbConnectionString = builder.Configuration["MongoDB:ConnnectionString"] ?? string.Empty;

TypeAdapterConfig typeAdapterConfig = null;
var options = new PetsOnTrailOptions()
{
    MongoDbConnectionString = MongoDbConnectionString ?? string.Empty,
    BackupPath = builder.Configuration["BackupDirectory:Path"] ?? string.Empty,
    SeedPath = builder.Configuration["SeedDirectory:Path"] ?? string.Empty
};

typeAdapterConfig = new TypeAdapterConfig
{
    RequireDestinationMemberSource = true,
    RequireExplicitMapping = true,
    Default =
    {
        Settings =
        {
            UseDestinationValues =
            {
                (member => member.SetterModifier == AccessModifier.None &&
                           member.Type.IsGenericType &&
                           member.Type.GetGenericTypeDefinition() == typeof(RepeatedField<>))
            },
            IgnoreNullValues = true
        }
    }
};

builder.Services
    .AddSingleton(typeAdapterConfig)
    .AddScoped<IMapper, ServiceMapper>()
    .AddScoped<JwtTokenInterceptor>()
    .AddLogging(config =>
    {
        config.AddConsole();
    });

builder.Services
    .AddStorage(new StorageOptions() { MongoDbConnectionString = options.MongoDbConnectionString, BackupPath = options.BackupPath, SeedPath = options.SeedPath }, typeAdapterConfig)
    .AddApiLayer(typeAdapterConfig, new PetsOnTrail.Actions.Options.PetsOnTrailOptions { MongoDbConnectionString = options.MongoDbConnectionString })
    .AddImport(typeAdapterConfig)
    .AddGrpc(options =>
    {
        options.Interceptors.Add<JwtTokenInterceptor>();
    });

typeAdapterConfig
    .AddActionRightsServiceMapping()
    .AddActionsServiceMapping()
    .AddUserProfilesServiceMapping()
    .AddEntriesServiceMapping()
    .AddPetsServiceMapping()
    .AddResultsMapping()
    .AddLiveUpdatesSubscriptionMapping()
    .AddCheckpointsServiceMapping()
    .AddCActivitiesServiceMapping();

typeAdapterConfig.NewConfig<Google.Type.Interval, Google.Type.Interval>();
typeAdapterConfig.NewConfig<Google.Protobuf.WellKnownTypes.Timestamp, Google.Protobuf.WellKnownTypes.Timestamp>();
typeAdapterConfig.NewConfig<Google.Type.DateTime, Google.Type.DateTime>();
typeAdapterConfig.NewConfig<Google.Protobuf.WellKnownTypes.Duration, Google.Protobuf.WellKnownTypes.Duration>();
typeAdapterConfig.NewConfig<Google.Type.TimeZone, Google.Type.TimeZone>();
typeAdapterConfig.NewConfig<Google.Type.LatLng, Google.Type.LatLng>();

typeAdapterConfig.NewConfig<DateTime, Google.Protobuf.WellKnownTypes.Timestamp>()
    .MapWith(s => new DateTimeOffset(s).ToGoogleTimestamp());

typeAdapterConfig.NewConfig<Google.Protobuf.WellKnownTypes.Timestamp, DateTime>()
    .MapWith(s => s.ToDateTime());

typeAdapterConfig.NewConfig<DateTime, Google.Type.DateTime>()
    .MapWith(s => new DateTimeOffset(s).ToGoogleDateTime());

typeAdapterConfig.NewConfig<DateTime?, Google.Type.DateTime>()
    .MapWith(s => s != null ? new DateTimeOffset(s.Value).ToGoogleDateTime() : null);

typeAdapterConfig.NewConfig<DateTimeOffset?, Google.Type.DateTime>()
    .MapWith(s => s != null ? s.ToGoogleDateTime() : null);
typeAdapterConfig.NewConfig<DateTimeOffset, Google.Type.DateTime>()
    .MapWith(s => s != null ? s.ToGoogleDateTime() : null);
            
typeAdapterConfig.NewConfig<Google.Type.DateTime, DateTimeOffset?>()
    .MapWith(s => s != null ? s.ToDateTimeOffset() : null);

typeAdapterConfig.NewConfig<Google.Protobuf.WellKnownTypes.Timestamp, DateTimeOffset?>()
    .MapWith(s => s != null ? s.ToDateTimeOffset() : null);

typeAdapterConfig.NewConfig<Google.Protobuf.WellKnownTypes.Timestamp, DateTimeOffset>()
    .MapWith(s => s.ToDateTimeOffset());

typeAdapterConfig.NewConfig<IList<string>, IdsRequest>()
    .MapWith(s => new IdsRequest { Ids = { s } });

typeAdapterConfig.NewConfig<IList<Guid>, IdsRequest>()
    .MapWith(s => new IdsRequest { Ids = { s.Select(x => x.ToString()) } });

typeAdapterConfig.NewConfig<Google.Type.DateTime, DateTimeOffset>()
    .MapWith(s => s.ToDateTimeOffset() ?? DateTimeOffset.MinValue);

TypeAdapterConfig.GlobalSettings.Compile();

builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding", "X-Grpc-Web", "User-Agent", "Access-Control-Allow-Origin");
}));

builder.Services.AddLocalization();

var app = builder.Build();

app.UseRouting();

app.UseGrpcWeb();

app.UseCors();

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
});

app.UseEndpoints(endpoints =>
    {
        endpoints.MapGrpcService<ActionsService>().EnableGrpcWeb().RequireCors("AllowAll");
        endpoints.MapGrpcService<UserProfilesService>().EnableGrpcWeb().RequireCors("AllowAll");
        endpoints.MapGrpcService<ActionRightsService>().EnableGrpcWeb().RequireCors("AllowAll");
        endpoints.MapGrpcService<EntriesService>().EnableGrpcWeb().RequireCors("AllowAll");
        endpoints.MapGrpcService<PetsService>().EnableGrpcWeb().RequireCors("AllowAll");
        endpoints.MapGrpcService<ResultsService>().EnableGrpcWeb().RequireCors("AllowAll");
        endpoints.MapGrpcService<LiveUpdatesSubscriptionGrpcService>().EnableGrpcWeb().RequireCors("AllowAll");
        endpoints.MapGrpcService<CheckpointsService>().EnableGrpcWeb().RequireCors("AllowAll");
        endpoints.MapGrpcService<ActivitiesService>().EnableGrpcWeb().RequireCors("AllowAll");
    }
);


await app.SeedDataAsync();

_ = app.ConfigureStorageAsync(CancellationToken.None);

await app.RunAsync();



