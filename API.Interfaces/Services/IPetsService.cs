﻿using DogsOnTrail.Interfaces.Actions.Entities.Pets;

namespace PetsOnTrail.Interfaces.Actions.Services;

public interface IPetsService
{
    Task<CreatePetResponse> CreatePetAsync(CreatePetRequest request, CancellationToken cancellationToken);

    Task<GetAllPetsResponse> GetAllPetsAsync(GetAllPetsRequest request, CancellationToken cancellationToken);

    Task<DeletePetResponse> DeletePetAsync(DeletePetRequest request, CancellationToken cancellationToken);

    Task<GetPetsFilteredByChipResponse> GetPetsFilteredByChipAsync(GetPetsFilteredByChipRequest request, CancellationToken cancellationToken);
}