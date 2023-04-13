﻿using DogtrekkingCzShared.Entities;
using Google.Protobuf.Collections;
using Mapster;

namespace DogtrekkingCzGRPCService.Services.Actions;

internal static class ActionsServiceMapping
{
    internal static TypeAdapterConfig AddAuthorizationServiceMapping(this TypeAdapterConfig typeAdapterConfig)
    {

        typeAdapterConfig.NewConfig<RepeatedField<Protos.Shared.ActionRights>, IReadOnlyList<ActionRightsDto>>();

        typeAdapterConfig.NewConfig<Protos.Shared.ActionRights, ActionRightsDto>();

        return typeAdapterConfig;
    }
}