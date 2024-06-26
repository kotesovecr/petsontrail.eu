﻿using Mapster;
using Storage.Entities.Activities;
using Storage.Models;

namespace Storage.Services.Repositories.Activities
{
    internal static class ActivitiesRepositoryMapping
    {
        internal static TypeAdapterConfig AddActivitiesRepositoryMapping(this TypeAdapterConfig typeAdapterConfig)
        {
            typeAdapterConfig.NewConfig<CreateActivityInternalStorageRequest, ActivityRecord>()
                .Map(d => d.CorrelationId, s => s.Id);
            typeAdapterConfig.NewConfig<CreateActivityInternalStorageRequest.PositionDto, ActivityRecord.PositionDto>();
            typeAdapterConfig.NewConfig<CreateActivityInternalStorageRequest.PetDto, ActivityRecord.PetDto>()
                .Map(d => d.Id, s => s.Id.ToString());
            typeAdapterConfig.NewConfig<ActivityRecord, CreateActivityInternalStorageResponse>();
            
            typeAdapterConfig.NewConfig<AddPointInternalStorageRequest, ActivityRecord.PositionDto>();
            typeAdapterConfig.NewConfig<ActivityRecord.PositionDto, AddPointInternalStorageResponse>();

            typeAdapterConfig.NewConfig<List<ActivityRecord>, GetActivitiesByUserIdInternalStorageResponse>()
                .Map(d => d.Activities, s => s)
                .Ignore(d => d.UserId);
            typeAdapterConfig.NewConfig<ActivityRecord, GetActivitiesByUserIdInternalStorageResponse.ActivityDto>();
            typeAdapterConfig.NewConfig<ActivityRecord.PositionDto, GetActivitiesByUserIdInternalStorageResponse.PositionDto>();
            typeAdapterConfig.NewConfig<ActivityRecord.PetDto, GetActivitiesByUserIdInternalStorageResponse.PetDto>();

            typeAdapterConfig.NewConfig<ActivityRecord, GetActivityByUserIdAndActivityIdInternalStorageResponse>();
            typeAdapterConfig.NewConfig<ActivityRecord.PositionDto, GetActivityByUserIdAndActivityIdInternalStorageResponse.PositionDto>();
            typeAdapterConfig.NewConfig<ActivityRecord.PetDto, GetActivityByUserIdAndActivityIdInternalStorageResponse.PetDto>();

            typeAdapterConfig.NewConfig<UserProfileRecord.ActivityDto, GetActivityByUserIdAndActivityIdInternalStorageResponse>()
                .Ignore(d => d.Positions)
                .Ignore(d => d.Pets);

            typeAdapterConfig.NewConfig<List<ActivityRecord>, GetActivitiesInternalStorageResponse>()
                .Map(d => d.Activities, s => s);
            typeAdapterConfig.NewConfig<ActivityRecord, GetActivitiesInternalStorageResponse.ActivityDto>();
            typeAdapterConfig.NewConfig<ActivityRecord.PositionDto, GetActivitiesInternalStorageResponse.PositionDto>();
            typeAdapterConfig.NewConfig<ActivityRecord.PetDto, GetActivitiesInternalStorageResponse.PetDto>();

            typeAdapterConfig.NewConfig<UpdateActivityInternalStorageRequest, ActivityRecord>()
                .Map(d => d.CorrelationId, s => s.Id);
            typeAdapterConfig.NewConfig<UpdateActivityInternalStorageRequest.PositionDto, ActivityRecord.PositionDto>();
            typeAdapterConfig.NewConfig<UpdateActivityInternalStorageRequest.PetDto, ActivityRecord.PetDto>();

            typeAdapterConfig.NewConfig<GetActivityByUserIdAndActivityIdInternalStorageResponse, UpdateActivityInternalStorageRequest>();
            typeAdapterConfig.NewConfig<GetActivityByUserIdAndActivityIdInternalStorageResponse.PositionDto, UpdateActivityInternalStorageRequest.PositionDto>();
            typeAdapterConfig.NewConfig<GetActivityByUserIdAndActivityIdInternalStorageResponse.PetDto, UpdateActivityInternalStorageRequest.PetDto>();
            typeAdapterConfig.NewConfig<GetActivityByUserIdAndActivityIdInternalStorageResponse, UpdateActivityInternalStorageRequest>();

            typeAdapterConfig.NewConfig<GetActivityByUserIdAndActivityIdInternalStorageResponse, CreateActivityInternalStorageRequest>();
            typeAdapterConfig.NewConfig<GetActivityByUserIdAndActivityIdInternalStorageResponse.PositionDto, CreateActivityInternalStorageRequest.PositionDto>();
            typeAdapterConfig.NewConfig<GetActivityByUserIdAndActivityIdInternalStorageResponse.PetDto, CreateActivityInternalStorageRequest.PetDto>();
            typeAdapterConfig.NewConfig<GetActivityByUserIdAndActivityIdInternalStorageResponse, CreateActivityInternalStorageRequest>();

            return typeAdapterConfig;
        }
    }
}