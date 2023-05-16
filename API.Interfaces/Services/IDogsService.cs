﻿using DogsOnTrail.Interfaces.Actions.Entities.Dogs;

namespace DogsOnTrail.Interfaces.Actions.Services;

public interface IDogsService
{
    Task<CreateDogResponse> CreateDogAsync(CreateDogRequest request, CancellationToken cancellationToken);

    Task<GetAllDogsResponse> GetAllDogsAsync(GetAllDogsRequest request, CancellationToken cancellationToken);

    Task<DeleteDogResponse> DeleteDogAsync(DeleteDogRequest request, CancellationToken cancellationToken);

    Task<GetDogResponse> GetDogAsync(GetDogRequest request, CancellationToken cancellationToken);
}