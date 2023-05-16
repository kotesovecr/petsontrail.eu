﻿using DogsOnTrail.Interfaces.Actions.Entities;
using DogsOnTrail.Interfaces.Actions.Entities.Actions;
using DogsOnTrail.Interfaces.Actions.Entities.Rights;
using SharedCode.Entities;
using Google.Protobuf.Collections;
using Mapster;
using Protos.Shared;
using Storage.Entities.Actions;
using DeleteActionRequest = DogsOnTrail.Interfaces.Actions.Entities.Actions.DeleteActionRequest;
using UpdateActionResponse = DogsOnTrail.Interfaces.Actions.Entities.Actions.UpdateActionResponse;

namespace DogsOnTrailGRPCService.Services.Authorization;

internal static class AuthorizationServiceMapping
{
    internal static TypeAdapterConfig AddAuthorizationServiceMapping(this TypeAdapterConfig typeAdapterConfig)
    {
        typeAdapterConfig.NewConfig<GetAllRightsResponse, RepeatedField<Protos.Shared.ActionRights>>()
            .MapWith(s => new RepeatedField<ActionRights>
            {
                s.Rights
                    .Select(r => new ActionRights
                    {
                        Id = r.Id,
                        Rights = (ActionRights.Types.RightsType) r.Rights,
                        ActionId = r.ActionId,
                        UserId = r.UserId,
                        Roles = { r.Roles }
                    })
                    .ToList()
            });
        
        return typeAdapterConfig;
    }
}