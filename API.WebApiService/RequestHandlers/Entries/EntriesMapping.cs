﻿using PetsOnTrail.Interfaces.Actions.Entities.Entries;
using Mapster;
using CreateEntryRequest = API.WebApiService.Entities.CreateEntryRequest;
using GetEntriesByActionRequest = API.WebApiService.Entities.GetEntriesByActionRequest;

namespace API.WebApiService.RequestHandlers.Entries;

internal static class EntriesMapping
{
    internal static TypeAdapterConfig AddEntriesMapping(this TypeAdapterConfig typeAdapterConfig)
    {
        typeAdapterConfig.NewConfig<CreateEntryRequest, PetsOnTrail.Interfaces.Actions.Entities.Entries.CreateEntryRequest>();
        typeAdapterConfig.NewConfig<CreateEntryResponse, Entities.CreateEntryResponse>();

        typeAdapterConfig.NewConfig<GetEntriesByActionRequest, PetsOnTrail.Interfaces.Actions.Entities.Entries.GetEntriesByActionRequest>();
        typeAdapterConfig.NewConfig<GetEntriesByActionResponse, Entities.GetEntriesByActionResponse>();
        typeAdapterConfig.NewConfig<GetEntriesByActionResponse.EntryDto, Entities.GetEntriesByActionResponse.EntryDto>();
        typeAdapterConfig.NewConfig<PetsOnTrail.Interfaces.Actions.Entities.Entries.CreateEntryRequest.VaccinationDto, Entities.GetEntriesByActionResponse.VaccinationDto>();
        typeAdapterConfig.NewConfig<PetsOnTrail.Interfaces.Actions.Entities.Entries.CreateEntryRequest.VaccinationType, Entities.GetEntriesByActionResponse.VaccinationType>();
        typeAdapterConfig.NewConfig<PetsOnTrail.Interfaces.Actions.Entities.Entries.CreateEntryRequest.PetDto, Entities.GetEntriesByActionResponse.PetDto>();
        typeAdapterConfig.NewConfig<PetsOnTrail.Interfaces.Actions.Entities.Entries.CreateEntryRequest.MerchandizeItemDto, Entities.GetEntriesByActionResponse.MerchandizeItemDto>();
        typeAdapterConfig.NewConfig<PetsOnTrail.Interfaces.Actions.Entities.Entries.CreateEntryRequest.AddressDto, Entities.GetEntriesByActionResponse.AddressDto>();

        return typeAdapterConfig;
    }
}