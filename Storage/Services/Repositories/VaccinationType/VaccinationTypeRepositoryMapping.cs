﻿using Amazon.Auth.AccessControlPolicy;
using Mapster;
using Storage.Entities.VaccinationTypes;
using Storage.Models;

namespace Storage.Services.Repositories.UserProfiles;

internal static class VaccinationTypeRepositoryMapping
{
    internal static TypeAdapterConfig AddVaccinationTypeRepositoryMapping(this TypeAdapterConfig typeAdapterConfig)
    {
        typeAdapterConfig.NewConfig<AddVaccinationTypeInternalStorageRequest, VaccinationTypeRecord>();

        typeAdapterConfig.NewConfig<VaccinationTypeRecord, GetAllVaccinationTypesInternalStorageResponse.VaccinationTypeDto>();

        return typeAdapterConfig;
    }
}