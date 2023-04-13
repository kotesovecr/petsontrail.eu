﻿using DogtrekkingCzShared.Entities;
using Mapster;

namespace DogtrekkingCzShared.Mapping
{
    public static class SharedMappingCategory
    {
        public static TypeAdapterConfig AddSharedMappingCategory(this TypeAdapterConfig typeAdapterConfig)
        {
            typeAdapterConfig.NewConfig<CategoryDto, Protos.Shared.Category>()
                .Map(d => d.Id, s => s.Id.ToString());

            typeAdapterConfig.NewConfig<Protos.Shared.Category, CategoryDto>()
                .Map(d => d.Id, s => Guid.Parse(s.Id));

            typeAdapterConfig.NewConfig<CategoryDto, CategoryDto>();

            return typeAdapterConfig;
        }
    }
}
