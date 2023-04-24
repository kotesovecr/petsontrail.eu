﻿using DogtrekkingCz.Interfaces.Actions.Entities.Dogs;
using DogtrekkingCz.Interfaces.Actions.Services;
using MapsterMapper;
using Storage.Entities.Dogs;
using Storage.Interfaces;
using GetAllDogsResponse = DogtrekkingCz.Interfaces.Actions.Entities.Dogs.GetAllDogsResponse;

namespace DogtrekkingCz.Actions.Services.DogsManage;

internal class DogsService : IDogsService
{
    private readonly IMapper _mapper;
    private readonly IDogsRepositoryService _dogsRepositoryService;
    
    public DogsService(IMapper mapper, IDogsRepositoryService dogsRepositoryService)
    {
        _mapper = mapper;
        _dogsRepositoryService = dogsRepositoryService;
    }
    
    public async Task<CreateDogResponse> CreateDogAsync(CreateDogRequest request, CancellationToken cancellationToken)
    {
        var addDogRequest = _mapper.Map<AddDogRequest>(request);
        
        var result = await _dogsRepositoryService.AddDogAsync(addDogRequest, cancellationToken);

        var response = _mapper.Map<CreateDogResponse>(result);

        return response;
    }

    public async Task<GetAllDogsResponse> GetAllDogsAsync(GetAllDogsRequest request, CancellationToken cancellationToken)
    {
        var result = await _dogsRepositoryService.GetAllAsync(cancellationToken);

        return new GetAllDogsResponse();
    }

    public async Task<DeleteDogResponse> DeleteDogAsync(DeleteDogRequest request, CancellationToken cancellationToken)
    {
        await _dogsRepositoryService.DeleteDogAsync(new DeleteDogInternalStorageRequest { Id = request.Id }, cancellationToken);

        return new DeleteDogResponse();
    }
}