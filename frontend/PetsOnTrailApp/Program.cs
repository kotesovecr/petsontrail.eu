using Blazored.Modal;
using Microsoft.AspNetCore.Components.Web;
using PetsOnTrailApp;
using PetsOnTrailApp.Extensions;
using PetsOnTrailApp.Services;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using MapsterMapper;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using SharedLib.Extensions;
using SharedLib.Providers;
using AppTokenProvider = PetsOnTrailApp.Providers.AppTokenProvider;
using TokenStorage = PetsOnTrailApp.Providers.TokenStorage;
using PetsOnTrailApp.Components;
using PetsOnTrailApp.Pages;
using PetsOnTrailApp.DataStorage;
using FisSst.BlazorMaps.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("OIDC", options.ProviderOptions);
});
builder.Services.AddBlazoredModal();
builder.Services.AddBlazorBootstrap();
builder.Services.AddBlazorLeafletMaps();

var typeAdapterConfig = SharedLib.Mapping.CreateFrontendMapping();
typeAdapterConfig
    .AddDataStorageMapping();

builder.Services
    .AddSingleton(typeAdapterConfig)
    .AddScoped<IMapper, ServiceMapper>()
    .AddSingleton<IUserProfileService, UserProfileService>()
    .AddBlazoredLocalStorage()
    .AddScoped<TokenStorage>()
    .AddScoped<ITokenProvider, AppTokenProvider>()
    .AddComponents()
    .AddPages()
    .AddDataStorage();

builder.Services.AddSingleton(services =>
{
    var baseUri = builder.Configuration["GrpcServerUri"];

    var channel = GrpcChannel.ForAddress(baseUri, new GrpcChannelOptions
    {
        HttpHandler = new GrpcWebHandler(new HttpClientHandler())
    });
    
    return channel;
});

builder.Services
    .AddAuthorizedGrpcOverWebClient<Protos.UserProfiles.UserProfiles.UserProfilesClient>(builder.Configuration["GrpcServerUri"])
    .AddAuthorizedGrpcOverWebClient<Protos.Actions.Actions.ActionsClient>(builder.Configuration["GrpcServerUri"])
    .AddAuthorizedGrpcOverWebClient<Protos.Entries.Entries.EntriesClient>(builder.Configuration["GrpcServerUri"])
    .AddAuthorizedGrpcOverWebClient<Protos.ActionRights.ActionRights.ActionRightsClient>(builder.Configuration["GrpcServerUri"])
    .AddAuthorizedGrpcOverWebClient<Protos.Pets.Pets.PetsClient>(builder.Configuration["GrpcServerUri"])
    .AddAuthorizedGrpcOverWebClient<Protos.Results.Results.ResultsClient>(builder.Configuration["GrpcServerUri"])
    .AddAuthorizedGrpcOverWebClient<Protos.LiveUpdatesSubscription.LiveUpdatesSubscription.LiveUpdatesSubscriptionClient>(builder.Configuration["GrpcServerUri"])
    .AddAuthorizedGrpcOverWebClient<Protos.Checkpoints.Checkpoints.CheckpointsClient>(builder.Configuration["GrpcServerUri"])
    .AddAuthorizedGrpcOverWebClient<Protos.Activities.Activities.ActivitiesClient>(builder.Configuration["GrpcServerUri"]);


builder.Services.AddLocalization();

var host = builder.Build();


await host.SetDefaultCulture();

await host.RunAsync();

