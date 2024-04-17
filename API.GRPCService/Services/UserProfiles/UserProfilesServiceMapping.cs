﻿using PetsOnTrail.Interfaces.Actions.Entities.UserProfile;
using Mapster;

namespace API.GRPCService.Services.UserProfiles;

internal static class UserProfilesServiceMapping
{
    internal static TypeAdapterConfig AddUserProfilesServiceMapping(this TypeAdapterConfig typeAdapterConfig)
    {
        typeAdapterConfig.NewConfig<GetUserProfileResponse, Protos.UserProfiles.GetUserProfile.GetUserProfileResponse>();
        typeAdapterConfig.NewConfig<GetUserProfileResponse.VaccinationType, Protos.UserProfiles.GetUserProfile.VaccinationType>();
        typeAdapterConfig.NewConfig<GetUserProfileResponse.AddressDto, Protos.UserProfiles.GetUserProfile.Address>();
        typeAdapterConfig.NewConfig<GetUserProfileResponse.ContactDto, Protos.UserProfiles.GetUserProfile.Contact>();
        typeAdapterConfig.NewConfig<GetUserProfileResponse.PetDto, Protos.UserProfiles.GetUserProfile.Pet>();
        typeAdapterConfig.NewConfig<GetUserProfileResponse.VaccinationDto, Protos.UserProfiles.GetUserProfile.Vaccination>();
        typeAdapterConfig.NewConfig<GetUserProfileResponse.LatLngDto, Google.Type.LatLng>();
        typeAdapterConfig.NewConfig<GetUserProfileResponse.ActivityDto, Protos.UserProfiles.GetUserProfile.Activity>();
        typeAdapterConfig.NewConfig<GetUserProfileResponse.PositionDto, Protos.UserProfiles.GetUserProfile.Position>();
        typeAdapterConfig.NewConfig<GetUserProfileResponse.ActivityPetDto, Protos.UserProfiles.GetUserProfile.ActivityPet>();

        typeAdapterConfig.NewConfig<Protos.UserProfiles.CreateUserProfile.CreateUserProfileRequest, CreateUserProfileRequest>()
            .Ignore(d => d.Roles);
        typeAdapterConfig.NewConfig<Protos.UserProfiles.CreateUserProfile.VaccinationType, CreateUserProfileRequest.VaccinationType>();
        typeAdapterConfig.NewConfig<Protos.UserProfiles.CreateUserProfile.Address, CreateUserProfileRequest.AddressDto>();
        typeAdapterConfig.NewConfig<Protos.UserProfiles.CreateUserProfile.Contact, CreateUserProfileRequest.ContactDto>();
        typeAdapterConfig.NewConfig<Protos.UserProfiles.CreateUserProfile.Pet, CreateUserProfileRequest.PetDto>();
        typeAdapterConfig.NewConfig<Protos.UserProfiles.CreateUserProfile.Vaccination, CreateUserProfileRequest.VaccinationDto>();
        typeAdapterConfig.NewConfig<Google.Type.LatLng, CreateUserProfileRequest.LatLngDto>();
        typeAdapterConfig.NewConfig<Protos.UserProfiles.CreateUserProfile.Activity, CreateUserProfileRequest.ActivityDto>();
        typeAdapterConfig.NewConfig<Protos.UserProfiles.CreateUserProfile.ActivityPet, CreateUserProfileRequest.ActivityPetDto>();
        typeAdapterConfig.NewConfig<Protos.UserProfiles.CreateUserProfile.Position, CreateUserProfileRequest.PositionDto>();
        typeAdapterConfig.NewConfig<CreateUserProfileResponse, Protos.UserProfiles.CreateUserProfile.CreateUserProfileResponse>();
        
        typeAdapterConfig.NewConfig<Protos.UserProfiles.UpdateUserProfile.UpdateUserProfileRequest, UpdateUserProfileRequest>();
        typeAdapterConfig.NewConfig<Protos.UserProfiles.UpdateUserProfile.VaccinationType, UpdateUserProfileRequest.VaccinationType>();
        typeAdapterConfig.NewConfig<Protos.UserProfiles.UpdateUserProfile.Address, UpdateUserProfileRequest.AddressDto>();
        typeAdapterConfig.NewConfig<Protos.UserProfiles.UpdateUserProfile.Contact, UpdateUserProfileRequest.ContactDto>();
        typeAdapterConfig.NewConfig<Protos.UserProfiles.UpdateUserProfile.Pet, UpdateUserProfileRequest.PetDto>();
        typeAdapterConfig.NewConfig<Protos.UserProfiles.UpdateUserProfile.Vaccination, UpdateUserProfileRequest.VaccinationDto>();
        typeAdapterConfig.NewConfig<Protos.UserProfiles.UpdateUserProfile.Activity, UpdateUserProfileRequest.ActivityDto>();
        typeAdapterConfig.NewConfig<Protos.UserProfiles.UpdateUserProfile.ActivityPet, UpdateUserProfileRequest.ActivityPetDto>();
        typeAdapterConfig.NewConfig<Protos.UserProfiles.UpdateUserProfile.Position, UpdateUserProfileRequest.PositionDto>();
        typeAdapterConfig.NewConfig<Google.Type.LatLng, UpdateUserProfileRequest.LatLngDto>();
        typeAdapterConfig.NewConfig<UpdateUserProfileResponse, Protos.UserProfiles.UpdateUserProfile.UpdateUserProfileResponse>();
        
        typeAdapterConfig.NewConfig<GetSelectedSurnameNameResponse.SelectedSurnameNameDto, Protos.UserProfiles.GetSelectedSurnameName.SelectedSurnameName>();
        
        return typeAdapterConfig;
    }
}