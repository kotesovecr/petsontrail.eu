﻿using Mapster;
using Storage.Entities.Actions;
using Storage.Entities.Entries;
using Storage.Models;

namespace Storage.Services.Repositories.Actions
{
    internal static class ActionRepositoryMapping
    {
        internal static TypeAdapterConfig AddActionRepositoryMapping(this TypeAdapterConfig typeAdapterConfig)
        {
            typeAdapterConfig.NewConfig<CreateActionInternalStorageRequest, ActionRecord>()
                .Ignore(d => d.CorrelationId);
            typeAdapterConfig.NewConfig<CreateActionInternalStorageRequest.RacerDto, ActionRecord.RacerDto>()
                .Ignore(d => d.PayedDate)
                .Ignore(d => d.AcceptedDate);
            typeAdapterConfig.NewConfig<CreateActionInternalStorageRequest.NoteDto, ActionRecord.NoteDto>();
            typeAdapterConfig.NewConfig<CreateActionInternalStorageRequest.PaymentDto, ActionRecord.PaymentDto>();
            typeAdapterConfig.NewConfig<CreateActionInternalStorageRequest.RequestedPaymentItem, ActionRecord.RequestedPaymentItem>();
            typeAdapterConfig.NewConfig<CreateActionInternalStorageRequest.RequestedPaymentsDto, ActionRecord.RequestedPaymentsDto>();
            typeAdapterConfig.NewConfig<CreateActionInternalStorageRequest.AddressDto, ActionRecord.AddressDto>();
            typeAdapterConfig.NewConfig<CreateActionInternalStorageRequest.CategoryDto, ActionRecord.CategoryDto>();
            typeAdapterConfig.NewConfig<CreateActionInternalStorageRequest.PetDto, ActionRecord.PetDto>();
            typeAdapterConfig.NewConfig<CreateActionInternalStorageRequest.LimitsDto, ActionRecord.LimitsDto>();
            typeAdapterConfig.NewConfig<CreateActionInternalStorageRequest.CheckpointDto, ActionRecord.CheckpointDto>();
            typeAdapterConfig.NewConfig<CreateActionInternalStorageRequest.RaceDto, ActionRecord.RaceDto>();
            typeAdapterConfig.NewConfig<CreateActionInternalStorageRequest.RaceState, ActionRecord.RaceState>();
            typeAdapterConfig.NewConfig<CreateActionInternalStorageRequest.TermDto, ActionRecord.TermDto>();
            typeAdapterConfig.NewConfig<CreateActionInternalStorageRequest.ActionSaleDto, ActionRecord.ActionSaleDto>();
            typeAdapterConfig.NewConfig<CreateActionInternalStorageRequest.LatLngDto, ActionRecord.LatLngDto>();
            typeAdapterConfig.NewConfig<CreateActionInternalStorageRequest.MerchandizeItemDto, ActionRecord.MerchandizeItemDto>();
            typeAdapterConfig.NewConfig<CreateActionInternalStorageRequest.PaymentDefinitionDto, ActionRecord.PaymentDefinitionDto>();
            typeAdapterConfig.NewConfig<CreateActionInternalStorageRequest.ActionSaleItemDto, ActionRecord.ActionSaleItemDto>();
            typeAdapterConfig.NewConfig<CreateActionInternalStorageRequest.PassedCheckpointDto, ActionRecord.PassedCheckpointDto>();

            typeAdapterConfig.NewConfig<UpdateActionInternalStorageRequest, ActionRecord>()
                .Ignore(d => d.CorrelationId);
            typeAdapterConfig.NewConfig<UpdateActionInternalStorageRequest.RacerDto, ActionRecord.RacerDto>();
            typeAdapterConfig.NewConfig<UpdateActionInternalStorageRequest.NoteDto, ActionRecord.NoteDto>();
            typeAdapterConfig.NewConfig<UpdateActionInternalStorageRequest.PaymentDto, ActionRecord.PaymentDto>();
            typeAdapterConfig.NewConfig<UpdateActionInternalStorageRequest.RequestedPaymentItem, ActionRecord.RequestedPaymentItem>();
            typeAdapterConfig.NewConfig<UpdateActionInternalStorageRequest.RequestedPaymentsDto, ActionRecord.RequestedPaymentsDto>();
            typeAdapterConfig.NewConfig<UpdateActionInternalStorageRequest.AddressDto, ActionRecord.AddressDto>();
            typeAdapterConfig.NewConfig<UpdateActionInternalStorageRequest.CategoryDto, ActionRecord.CategoryDto>();
            typeAdapterConfig.NewConfig<UpdateActionInternalStorageRequest.PetDto, ActionRecord.PetDto>();
            typeAdapterConfig.NewConfig<UpdateActionInternalStorageRequest.LimitsDto, ActionRecord.LimitsDto>();
            typeAdapterConfig.NewConfig<UpdateActionInternalStorageRequest.CheckpointDto, ActionRecord.CheckpointDto>();
            typeAdapterConfig.NewConfig<UpdateActionInternalStorageRequest.RaceDto, ActionRecord.RaceDto>();
            typeAdapterConfig.NewConfig<UpdateActionInternalStorageRequest.RaceState, ActionRecord.RaceState>();
            typeAdapterConfig.NewConfig<UpdateActionInternalStorageRequest.TermDto, ActionRecord.TermDto>();
            typeAdapterConfig.NewConfig<UpdateActionInternalStorageRequest.ActionSaleDto, ActionRecord.ActionSaleDto>();
            typeAdapterConfig.NewConfig<UpdateActionInternalStorageRequest.LatLngDto, ActionRecord.LatLngDto>();
            typeAdapterConfig.NewConfig<UpdateActionInternalStorageRequest.MerchandizeItemDto, ActionRecord.MerchandizeItemDto>();
            typeAdapterConfig.NewConfig<UpdateActionInternalStorageRequest.PaymentDefinitionDto, ActionRecord.PaymentDefinitionDto>();
            typeAdapterConfig.NewConfig<UpdateActionInternalStorageRequest.ActionSaleItemDto, ActionRecord.ActionSaleItemDto>();
            typeAdapterConfig.NewConfig<UpdateActionInternalStorageRequest.PassedCheckpointDto, ActionRecord.PassedCheckpointDto>();

            typeAdapterConfig.NewConfig<GetActionInternalStorageResponse, UpdateActionInternalStorageRequest>();
            typeAdapterConfig.NewConfig<GetActionInternalStorageResponse.RacerDto, UpdateActionInternalStorageRequest.RacerDto>();
            typeAdapterConfig.NewConfig<GetActionInternalStorageResponse.NoteDto, UpdateActionInternalStorageRequest.NoteDto>();
            typeAdapterConfig.NewConfig<GetActionInternalStorageResponse.PaymentDto, UpdateActionInternalStorageRequest.PaymentDto>();
            typeAdapterConfig.NewConfig<GetActionInternalStorageResponse.RequestedPaymentItem, UpdateActionInternalStorageRequest.RequestedPaymentItem>();
            typeAdapterConfig.NewConfig<GetActionInternalStorageResponse.RequestedPaymentsDto, UpdateActionInternalStorageRequest.RequestedPaymentsDto>();
            typeAdapterConfig.NewConfig<GetActionInternalStorageResponse.AddressDto, UpdateActionInternalStorageRequest.AddressDto>();
            typeAdapterConfig.NewConfig<GetActionInternalStorageResponse.CategoryDto, UpdateActionInternalStorageRequest.CategoryDto>();
            typeAdapterConfig.NewConfig<GetActionInternalStorageResponse.PetDto, UpdateActionInternalStorageRequest.PetDto>();
            typeAdapterConfig.NewConfig<GetActionInternalStorageResponse.LimitsDto, UpdateActionInternalStorageRequest.LimitsDto>();
            typeAdapterConfig.NewConfig<GetActionInternalStorageResponse.CheckpointDto, UpdateActionInternalStorageRequest.CheckpointDto>();
            typeAdapterConfig.NewConfig<GetActionInternalStorageResponse.RaceDto, UpdateActionInternalStorageRequest.RaceDto>();
            typeAdapterConfig.NewConfig<GetActionInternalStorageResponse.RaceState, UpdateActionInternalStorageRequest.RaceState>();
            typeAdapterConfig.NewConfig<GetActionInternalStorageResponse.TermDto, UpdateActionInternalStorageRequest.TermDto>();
            typeAdapterConfig.NewConfig<GetActionInternalStorageResponse.ActionSaleDto, UpdateActionInternalStorageRequest.ActionSaleDto>();
            typeAdapterConfig.NewConfig<GetActionInternalStorageResponse.LatLngDto, UpdateActionInternalStorageRequest.LatLngDto>();
            typeAdapterConfig.NewConfig<GetActionInternalStorageResponse.MerchandizeItemDto, UpdateActionInternalStorageRequest.MerchandizeItemDto>();
            typeAdapterConfig.NewConfig<GetActionInternalStorageResponse.PaymentDefinitionDto, UpdateActionInternalStorageRequest.PaymentDefinitionDto>();
            typeAdapterConfig.NewConfig<GetActionInternalStorageResponse.ActionSaleItemDto, UpdateActionInternalStorageRequest.ActionSaleItemDto>();
            typeAdapterConfig.NewConfig<GetActionInternalStorageResponse.PassedCheckpointDto, UpdateActionInternalStorageRequest.PassedCheckpointDto>();

            typeAdapterConfig.NewConfig<ActionRecord, GetAllActionsInternalStorageResponse.ActionDto>();
            typeAdapterConfig.NewConfig<ActionRecord.RacerDto, GetAllActionsInternalStorageResponse.RacerDto>();
            typeAdapterConfig.NewConfig<ActionRecord.NoteDto, GetAllActionsInternalStorageResponse.NoteDto>();
            typeAdapterConfig.NewConfig<ActionRecord.PaymentDto, GetAllActionsInternalStorageResponse.PaymentDto>();
            typeAdapterConfig.NewConfig<ActionRecord.RequestedPaymentItem, GetAllActionsInternalStorageResponse.RequestedPaymentItem>();
            typeAdapterConfig.NewConfig<ActionRecord.RequestedPaymentsDto, GetAllActionsInternalStorageResponse.RequestedPaymentsDto>();
            typeAdapterConfig.NewConfig<ActionRecord.AddressDto, GetAllActionsInternalStorageResponse.AddressDto>();
            typeAdapterConfig.NewConfig<ActionRecord.CategoryDto, GetAllActionsInternalStorageResponse.CategoryDto>();
            typeAdapterConfig.NewConfig<ActionRecord.PetDto, GetAllActionsInternalStorageResponse.PetDto>();
            typeAdapterConfig.NewConfig<ActionRecord.LimitsDto, GetAllActionsInternalStorageResponse.LimitsDto>();
            typeAdapterConfig.NewConfig<ActionRecord.CheckpointDto, GetAllActionsInternalStorageResponse.CheckpointDto>();
            typeAdapterConfig.NewConfig<ActionRecord.RaceDto, GetAllActionsInternalStorageResponse.RaceDto>();
            typeAdapterConfig.NewConfig<ActionRecord.RaceState, GetAllActionsInternalStorageResponse.RaceState>();
            typeAdapterConfig.NewConfig<ActionRecord.TermDto, GetAllActionsInternalStorageResponse.TermDto>();
            typeAdapterConfig.NewConfig<ActionRecord.ActionSaleDto, GetAllActionsInternalStorageResponse.ActionSaleDto>();
            typeAdapterConfig.NewConfig<ActionRecord.LatLngDto, GetAllActionsInternalStorageResponse.LatLngDto>();
            typeAdapterConfig.NewConfig<ActionRecord.MerchandizeItemDto, GetAllActionsInternalStorageResponse.MerchandizeItemDto>();
            typeAdapterConfig.NewConfig<ActionRecord.PaymentDefinitionDto, GetAllActionsInternalStorageResponse.PaymentDefinitionDto>();
            typeAdapterConfig.NewConfig<ActionRecord.ActionSaleItemDto, GetAllActionsInternalStorageResponse.ActionSaleItemDto>();
            typeAdapterConfig.NewConfig<ActionRecord.PassedCheckpointDto, GetAllActionsInternalStorageResponse.PassedCheckpointDto>();
            
            typeAdapterConfig.NewConfig<ActionRecord, GetSelectedActionsInternalStorageResponse.ActionDto>();
            typeAdapterConfig.NewConfig<ActionRecord.RacerDto, GetSelectedActionsInternalStorageResponse.RacerDto>();
            typeAdapterConfig.NewConfig<ActionRecord.NoteDto, GetSelectedActionsInternalStorageResponse.NoteDto>();
            typeAdapterConfig.NewConfig<ActionRecord.PaymentDto, GetSelectedActionsInternalStorageResponse.PaymentDto>();
            typeAdapterConfig.NewConfig<ActionRecord.RequestedPaymentItem, GetSelectedActionsInternalStorageResponse.RequestedPaymentItem>();
            typeAdapterConfig.NewConfig<ActionRecord.RequestedPaymentsDto, GetSelectedActionsInternalStorageResponse.RequestedPaymentsDto>();
            typeAdapterConfig.NewConfig<ActionRecord.AddressDto, GetSelectedActionsInternalStorageResponse.AddressDto>();
            typeAdapterConfig.NewConfig<ActionRecord.CategoryDto, GetSelectedActionsInternalStorageResponse.CategoryDto>();
            typeAdapterConfig.NewConfig<ActionRecord.PetDto, GetSelectedActionsInternalStorageResponse.PetDto>();
            typeAdapterConfig.NewConfig<ActionRecord.LimitsDto, GetSelectedActionsInternalStorageResponse.LimitsDto>();
            typeAdapterConfig.NewConfig<ActionRecord.CheckpointDto, GetSelectedActionsInternalStorageResponse.CheckpointDto>();
            typeAdapterConfig.NewConfig<ActionRecord.RaceDto, GetSelectedActionsInternalStorageResponse.RaceDto>();
            typeAdapterConfig.NewConfig<ActionRecord.RaceState, GetSelectedActionsInternalStorageResponse.RaceState>();
            typeAdapterConfig.NewConfig<ActionRecord.TermDto, GetSelectedActionsInternalStorageResponse.TermDto>();
            typeAdapterConfig.NewConfig<ActionRecord.ActionSaleDto, GetSelectedActionsInternalStorageResponse.ActionSaleDto>();
            typeAdapterConfig.NewConfig<ActionRecord.LatLngDto, GetSelectedActionsInternalStorageResponse.LatLngDto>();
            typeAdapterConfig.NewConfig<ActionRecord.MerchandizeItemDto, GetSelectedActionsInternalStorageResponse.MerchandizeItemDto>();
            typeAdapterConfig.NewConfig<ActionRecord.PaymentDefinitionDto, GetSelectedActionsInternalStorageResponse.PaymentDefinitionDto>();
            typeAdapterConfig.NewConfig<ActionRecord.ActionSaleItemDto, GetSelectedActionsInternalStorageResponse.ActionSaleItemDto>();
            typeAdapterConfig.NewConfig<ActionRecord.PassedCheckpointDto, GetSelectedActionsInternalStorageResponse.PassedCheckpointDto>();

            typeAdapterConfig.NewConfig<ActionRecord, CreateActionInternalStorageResponse>();

            typeAdapterConfig.NewConfig<ActionRecord, GetActionInternalStorageResponse>(); 
            typeAdapterConfig.NewConfig<ActionRecord.RacerDto, GetActionInternalStorageResponse.RacerDto>();
            typeAdapterConfig.NewConfig<ActionRecord.NoteDto, GetActionInternalStorageResponse.NoteDto>();
            typeAdapterConfig.NewConfig<ActionRecord.PaymentDto, GetActionInternalStorageResponse.PaymentDto>();
            typeAdapterConfig.NewConfig<ActionRecord.RequestedPaymentItem, GetActionInternalStorageResponse.RequestedPaymentItem>();
            typeAdapterConfig.NewConfig<ActionRecord.RequestedPaymentsDto, GetActionInternalStorageResponse.RequestedPaymentsDto>();
            typeAdapterConfig.NewConfig<ActionRecord.AddressDto, GetActionInternalStorageResponse.AddressDto>();
            typeAdapterConfig.NewConfig<ActionRecord.CategoryDto, GetActionInternalStorageResponse.CategoryDto>();
            typeAdapterConfig.NewConfig<ActionRecord.PetDto, GetActionInternalStorageResponse.PetDto>();
            typeAdapterConfig.NewConfig<ActionRecord.LimitsDto, GetActionInternalStorageResponse.LimitsDto>();
            typeAdapterConfig.NewConfig<ActionRecord.CheckpointDto, GetActionInternalStorageResponse.CheckpointDto>();
            typeAdapterConfig.NewConfig<ActionRecord.RaceDto, GetActionInternalStorageResponse.RaceDto>();
            typeAdapterConfig.NewConfig<ActionRecord.RaceState, GetActionInternalStorageResponse.RaceState>();
            typeAdapterConfig.NewConfig<ActionRecord.TermDto, GetActionInternalStorageResponse.TermDto>();
            typeAdapterConfig.NewConfig<ActionRecord.ActionSaleDto, GetActionInternalStorageResponse.ActionSaleDto>();
            typeAdapterConfig.NewConfig<ActionRecord.LatLngDto, GetActionInternalStorageResponse.LatLngDto>();
            typeAdapterConfig.NewConfig<ActionRecord.MerchandizeItemDto, GetActionInternalStorageResponse.MerchandizeItemDto>();
            typeAdapterConfig.NewConfig<ActionRecord.PaymentDefinitionDto, GetActionInternalStorageResponse.PaymentDefinitionDto>();
            typeAdapterConfig.NewConfig<ActionRecord.ActionSaleItemDto, GetActionInternalStorageResponse.ActionSaleItemDto>();
            typeAdapterConfig.NewConfig<ActionRecord.PassedCheckpointDto, GetActionInternalStorageResponse.PassedCheckpointDto>();

            typeAdapterConfig.NewConfig<AddResultInternalStorageRequest, ActionRecord.RacerDto>()
                .Ignore(d => d.Id)
                .Ignore(d => d.CompetitorId)
                .Ignore(d => d.Payments)
                .Ignore(d => d.Notes)
                .Ignore(d => d.RequestedPayments)
                .Ignore(d => d.Merchandize)
                .Ignore(d => d.Address)
                .Ignore(d => d.Pets)
                .Ignore(d => d.PassedCheckpoints)
                .Ignore(d => d.CheckpointData)
                .Ignore(d => d.PayedDate)
                .Map(d => d.Accepted, s => true)
                .Map(d => d.AcceptedDate, s => DateTime.Now)
                .Map(d => d.Payed, s => false);

            typeAdapterConfig.NewConfig<GetEntryInternalStorageResponse, UpdateActionInternalStorageRequest.RacerDto>()
                .Ignore(d => d.CheckpointData)
                .Ignore(d => d.Start)
                .Ignore(d => d.Finish)
                .Ignore(d => d.Payed)
                .Ignore(d => d.PayedDate)
                .Ignore(d => d.Payments)
                .Ignore(d => d.RequestedPayments)
                .Ignore(d => d.PassedCheckpoints);
            typeAdapterConfig.NewConfig<GetEntryInternalStorageResponse.MerchandizeItemDto, UpdateActionInternalStorageRequest.MerchandizeItemDto>();
            typeAdapterConfig.NewConfig<GetEntryInternalStorageResponse.AddressDto, UpdateActionInternalStorageRequest.AddressDto>();
            typeAdapterConfig.NewConfig<GetEntryInternalStorageResponse.LatLngDto, UpdateActionInternalStorageRequest.LatLngDto>();
            typeAdapterConfig.NewConfig<GetEntryInternalStorageResponse.PetDto, UpdateActionInternalStorageRequest.PetDto>()
                .Ignore(d => d.UserId)
                .Ignore(d => d.Kennel)
                .Ignore(d => d.UriToPhoto)
                .Ignore(d => d.Contact);

            typeAdapterConfig.NewConfig<ActionRecord, GetSimpleActionsListByTypeInternalStorageResponse.ActionDto>()
                .MapWith((src) => new GetSimpleActionsListByTypeInternalStorageResponse.ActionDto
                {
                    Id = src.Id,
                    Begin = src.Term.From.DateTime,
                    End = src.Term.To.DateTime,
                    City = src.Address.City,
                    Name = src.Name,
                    TypeId = src.TypeId,
                    Description = src.Description,
                    Races = src.Races
                                .Select(race => new GetSimpleActionsListByTypeInternalStorageResponse.RaceDto { Id = race.Id, Name = race.Name })
                                .ToList()
                });

            return typeAdapterConfig;
        }
    }
}
