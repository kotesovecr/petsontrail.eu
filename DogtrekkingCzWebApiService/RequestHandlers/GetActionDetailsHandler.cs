﻿using DogtrekkingCz.Interfaces.Actions.Services;
using DogtrekkingCzWebApiService.Entities;
using MapsterMapper;
using Mediator;

namespace DogtrekkingCzWebApiService.RequestHandlers
{
    public sealed class GetActionDetailsHandler : IRequestHandler<ActionDetailRequest, ActionDetailResponse>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public GetActionDetailsHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async ValueTask<ActionDetailResponse> Handle(ActionDetailRequest request, CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

                var actionsService = scope.ServiceProvider.GetRequiredService<IActionsService>();

                var actionDetailRequest = mapper.Map<DogtrekkingCz.Interfaces.Actions.Entities.ActionDetailRequest>(request);

                var action = await actionsService.GetActionDetail(actionDetailRequest, cancellationToken);

                return new ValueTask<ActionDetailResponse>(new ActionDetailResponse(Guid.NewGuid())).Result;
            }
        }
    }
}
