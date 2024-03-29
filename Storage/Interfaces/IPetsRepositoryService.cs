﻿using Storage.Entities.Pets;

namespace Storage.Interfaces;

public interface IPetsRepositoryService : IRepositoryService
{
    Task<AddPetInternalStorageResponse> AddPetAsync(CreatePetInternalStorageRequest request, CancellationToken cancellationToken);

    Task<UpdatePetInternalStorageResponse> UpdatePetAsync(UpdatePetInternalStorageRequest request, CancellationToken cancellationToken);

    Task<GetPetsFilteredByChipInternalStorageResponse> GetPetsFilteredByChipAsync(GetPetsFilteredByChipInternalStorageRequest request, CancellationToken cancellationToken);
    
    Task DeletePetAsync(DeletePetInternalStorageRequest request, CancellationToken cancellationToken);

    Task<GetAllPetsInternalStorageResponse> GetAllAsync(CancellationToken cancellationToken);
    
    Task<GetPetInternalStorageResponse>  GetPetAsync(GetPetInternalStorageRequest request, CancellationToken cancellationToken);
}
