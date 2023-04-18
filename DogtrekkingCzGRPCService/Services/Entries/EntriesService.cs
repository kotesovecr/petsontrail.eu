﻿using DogtrekkingCz.Interfaces.Actions.Services;
using Grpc.Core;
using MapsterMapper;
using Protos.Entries;
using CreateEntryRequest = DogtrekkingCz.Interfaces.Actions.Entities.Entries.CreateEntryRequest;
using DeleteEntryRequest = DogtrekkingCz.Interfaces.Actions.Entities.Entries.DeleteEntryRequest;
using GetAllEntriesRequest = DogtrekkingCz.Interfaces.Actions.Entities.Entries.GetAllEntriesRequest;
using GetEntriesByActionRequest = DogtrekkingCz.Interfaces.Actions.Entities.Entries.GetEntriesByActionRequest;

namespace DogtrekkingCzGRPCService.Services.Entries;

internal class EntriesService : Protos.Entries.Entries.EntriesBase
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IEntriesService _entriesService;

    public EntriesService(ILogger<EntriesService> logger, IMapper mapper, IEntriesService entriesService)
    {
        _logger = logger;
        _mapper = mapper;
        _entriesService = entriesService;
    }

    public async override Task<Protos.Entries.CreateEntryResponse> createEntry(Protos.Entries.CreateEntryRequest request, ServerCallContext context)
    {
        var createEntryRequest = _mapper.Map<CreateEntryRequest>(request);
        
        var newEntry = await _entriesService.CreateEntryAsync(createEntryRequest, context.CancellationToken);

        var response = _mapper.Map<Protos.Entries.CreateEntryResponse>(newEntry);

        return response;
    }

    public async override Task<Protos.Entries.GetEntriesByActionResponse> getEntriesByAction(Protos.Entries.GetEntriesByActionRequest request, ServerCallContext context)
    {
        var getEntriesByActionRequest = _mapper.Map<GetEntriesByActionRequest>(request);

        var entries = await _entriesService.GetEntriesByActionAsync(getEntriesByActionRequest, context.CancellationToken);

        var response = _mapper.Map<Protos.Entries.GetEntriesByActionResponse>(entries);

        return response;
    }

    public async override Task<Protos.Entries.GetAllEntriesResponse> getAllEntries(Protos.Entries.GetAllEntriesRequest request, ServerCallContext context)
    {
        var getAllEntriesRequest = _mapper.Map<GetAllEntriesRequest>(request);

        var entries = await _entriesService.GetAllEntriesAsync(getAllEntriesRequest, context.CancellationToken);

        var response = _mapper.Map<Protos.Entries.GetAllEntriesResponse>(entries);

        return response;
    }

    public async override Task<Protos.Entries.DeleteEntryResponse> deleteEntry(Protos.Entries.DeleteEntryRequest request, ServerCallContext context)
    {
        var deleteEntryRequest = _mapper.Map<DeleteEntryRequest>(request);

        await _entriesService.DeleteEntryAsync(deleteEntryRequest, context.CancellationToken);

        return new DeleteEntryResponse();
    }
}