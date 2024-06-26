﻿using API.WebApiService.Entities;
using PetsOnTrail.Interfaces.Actions.Services;
using MapsterMapper;
using Mediator;
using PetsOnTrail.Actions.Extensions;

namespace API.WebApiService.RequestHandlers.Activities;

public sealed class CreateActivityHandler : IRequestHandler<CreateActivityRequest, CreateActivityResponse>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IMapper _mapper;
    private readonly IActivitiesService _activitiesService;
    private readonly ILogger<CreateActivityHandler> _logger;

    public CreateActivityHandler(IServiceScopeFactory serviceScopeFactory, IMapper mapper, IActivitiesService activitiesService, ILogger<CreateActivityHandler> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _mapper = mapper;
        _activitiesService = activitiesService;
        _logger = logger;
    }

    public async ValueTask<CreateActivityResponse> Handle(CreateActivityRequest request, CancellationToken cancellationToken)
    {
        //using (var scope = _serviceScopeFactory.CreateScope())
        //{
            //_mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

           // var activitiesService = scope.ServiceProvider.GetRequiredService<IActivitiesService>();

            _logger.LogInformation($"WebApi: CreateActivityHandler: {request.Dump()}");

            var createActivitiesRequest =
                _mapper.Map<PetsOnTrail.Interfaces.Actions.Entities.Activities.CreateActivityRequest>(request);

            _logger.LogInformation($"WebApi: CreateActivityHandler: {createActivitiesRequest.Dump()}");

            var newEntry = await _activitiesService.CreateActivityAsync(createActivitiesRequest, cancellationToken);

            var entry = _mapper.Map<CreateActivityResponse>(newEntry);
            
            return new ValueTask<CreateActivityResponse>(entry).Result;
        //}
    }
}
