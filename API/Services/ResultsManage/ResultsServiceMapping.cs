﻿using PetsOnTrail.Interfaces.Actions.Entities.Entries;
using PetsOnTrail.Interfaces.Actions.Entities.Results;
using Mapster;
using Storage.Entities.Actions;
using Storage.Entities.Entries;

namespace PetsOnTrail.Actions.Services.ResultsManage;

internal static class ResultsServiceMapping
{
    public static TypeAdapterConfig AddResultsMapping(this TypeAdapterConfig typeAdapterConfig)
    {
        typeAdapterConfig.NewConfig<AddResultRequest, AddResultInternalStorageRequest>()
            .Ignore(d => d.Id)
            .Ignore(d => d.UserId);
        typeAdapterConfig.NewConfig<AddResultRequest.FinalState, AddResultInternalStorageRequest.FinalState>();

        typeAdapterConfig.NewConfig<AddResultInternalStorageResponse, AddResultResponse>();
        
        return typeAdapterConfig;
    }
}