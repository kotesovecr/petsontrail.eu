﻿using Storage.Seed;

namespace DogsOnTrailGRPCService.Extensions
{
    public static class SeedDataExtension
    {
        public static async Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder builder)
        {
            var storageSeed = builder.ApplicationServices.GetRequiredService<StorageSeedEngine>();
            if (storageSeed != null) 
            {
                await storageSeed.SeedAsync();
            }

            return builder;
        }
    }
}