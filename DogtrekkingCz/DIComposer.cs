﻿using DogtrekkingCz.Interfaces.Actions.Services;
using DogtrekkingCz.Storage;
using DogtrekkingCzShared.Interceptors;
using DogtrekkingCzShared.Options;
using DogtrekkingCzShared.Services.JwtToken;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Storage.Interfaces.Options;

namespace DogtrekkingCz.Actions
{
    public static class DIComposer
    {
        public static IServiceCollection AddActions(this IServiceCollection services, TypeAdapterConfig typeAdapterConfig, DogtrekkingCzOptions options)
        {
            services
                .AddScoped<IJwtTokenService, JwtTokenService>()
                .AddScoped<JwtTokenInterceptor>()
                .AddStorage(new StorageOptions() { MongoDbConnectionString = options.MongoDbConnectionString }, typeAdapterConfig);

            services.AddScoped<IActionsService, ActionsService>();

            return services;
        }
    }
}
