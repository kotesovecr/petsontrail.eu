﻿using MapsterMapper;
using Storage.Entities.ActionRights;
using Storage.Interfaces;
using Storage.Interfaces.Services;
using Storage.Models;

namespace Storage.Services.Repositories.ActionRights
{
    internal class ActionRightsRepositoryService : IActionRightsRepositoryService
    {
        private readonly IMapper _mapper;
        private readonly IStorageService<ActionRightsRecord> _actionRightsStorageService;

        public ActionRightsRepositoryService(IMapper mapper, IStorageService<ActionRightsRecord> actionRigtsStorageService)
        {
            _mapper = mapper;
            _actionRightsStorageService = actionRigtsStorageService;
        }

        public async Task<AddActionRightsResponse> AddActionRightsAsync(AddActionRightsRequest request)
        {
            var addRequest = _mapper.Map<ActionRightsRecord>(request);

            if (addRequest.Id == null)
            {
                addRequest.Id = Guid.NewGuid().ToString();
            }

            var addedActionRightsRecord = await _actionRightsStorageService.AddAsync(addRequest);

            var response = new AddActionRightsResponse
            {
                Id = addedActionRightsRecord?.Id ?? ""
            };

            return response;
        }

        public Task DeleteActionRightsAsync(DeleteActionRightsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetActionRightsResponse> GetActionRightsAsync(GetActionRightsRequest request)
        {
            var result = await _actionRightsStorageService.GetByFilterAsync("UserId", request.UserId);

            if (result == null)
                return null;

            var response = _mapper.Map<GetActionRightsResponse>(result);

            return response;
        }
    }
}