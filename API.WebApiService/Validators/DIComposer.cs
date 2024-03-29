﻿using API.WebApiService.Entities;
using Mediator;

namespace API.WebApiService.Validators
{
    public static class DIComposerValidators
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddSingleton<IPipelineBehavior<ActionDetailRequest, ActionDetailResponse>, ActionDetailValidator>();
            services.AddSingleton<IPipelineBehavior<CreateEntryRequest, CreateEntryResponse>, CreateEntryValidator>();
            services.AddSingleton<IPipelineBehavior<GetEntriesByActionRequest, GetEntriesByActionResponse>, GetEntriesByActionValidator>();

            return services;
        }
    }
}
