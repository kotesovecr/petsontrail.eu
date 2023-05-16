﻿using DogsOnTrail.Interfaces.Actions.Entities.Entries;
using DogsOnTrail.Interfaces.Actions.Entities.Results;
using Mapster;
using Storage.Entities.Actions;
using Storage.Entities.Entries;

namespace DogsOnTrail.Actions.Services.ResultsManage;

internal static class ResultsServiceMapping
{
    public static TypeAdapterConfig AddResultsMapping(this TypeAdapterConfig typeAdapterConfig)
    {
        typeAdapterConfig.NewConfig<AddResultRequest, AddResultInternalStorageRequest>();

        typeAdapterConfig.NewConfig<AddResultInternalStorageResponse, AddResultResponse>();
        
        return typeAdapterConfig;
    }
}