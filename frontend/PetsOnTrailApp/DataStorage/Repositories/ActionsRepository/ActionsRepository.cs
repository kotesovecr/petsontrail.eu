﻿using MapsterMapper;
using PetsOnTrailApp.Extensions;
using PetsOnTrailApp.Models;
using PetsOnTrailApp.Services;
using Protos.Actions.GetSelectedPublicActionsList;
using Protos.Actions.GetSimpleActionsList;
using static Protos.Actions.Actions;

namespace PetsOnTrailApp.DataStorage.Repositories.ActionsRepository;

public class ActionsRepository : BaseRepository, IActionsRepository
{
    private readonly IDataStorageService<GetSelectedPublicActionsListResponse, GetSelectedPublicActionsListResponseModel> _dataStorageServicePublicActions;
    private readonly IDataStorageService<GetSimpleActionsListResponse, GetSimpleActionsListResponseModel> _dataStorageServiceActionsByType;
    private readonly IMapper _mapper;

    private static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

    private Dictionary<Guid, ActionModel> _actions = new Dictionary<Guid, ActionModel>();
    private Dictionary<Guid, IList<SimpleActionModel>> _actionsSimple = new Dictionary<Guid, IList<SimpleActionModel>>();
    private Dictionary<Guid, RacesModel> _races = new Dictionary<Guid, RacesModel>();
    private Dictionary<(Guid actionId, Guid raceId), RaceModel> _race = new Dictionary<(Guid, Guid), RaceModel>();
    private Dictionary<(Guid actionId, Guid raceId), CategoriesModel> _categories = new Dictionary<(Guid, Guid), CategoriesModel>();
    private Dictionary<(Guid actionId, Guid raceId, Guid categoryId), ResultsModel> _results = new Dictionary<(Guid, Guid, Guid), ResultsModel>();

    public ActionsRepository(
        IDataStorageService<GetSelectedPublicActionsListResponse, GetSelectedPublicActionsListResponseModel> dataStorageServicePublicActions,
        IDataStorageService<GetSimpleActionsListResponse, GetSimpleActionsListResponseModel> dataStorageServiceActionsByType,
        IMapper mapper,
        ActionsClient actionsClient,
        IUserProfileService userProfileService) : base(actionsClient, userProfileService)
    {
        _dataStorageServicePublicActions = dataStorageServicePublicActions;
        _dataStorageServiceActionsByType = dataStorageServiceActionsByType;
        _mapper = mapper;
    }

    public async Task<IList<SimpleActionModel>> GetAllActionsByTypeAsync(IList<Guid> typeIds, CancellationToken cancellationToken)
    {
        List<SimpleActionModel> result = new List<SimpleActionModel>();

        await semaphoreSlim.WaitAsync();
        try
        {
            foreach (var typeId in typeIds)
            {
                var data = _actionsSimple.GetValueOrDefault(typeId, default(IList<SimpleActionModel>));
                
                if (data is not null)
                    result.AddRange(data);
            }
        }
        finally
        {
            semaphoreSlim.Release();
        }


        await LoadAndParseActionsSimpleAsync(typeIds, cancellationToken);

        await semaphoreSlim.WaitAsync();
        try
        {
            foreach (var typeId in typeIds)
            {
                var data = _actionsSimple.GetValueOrDefault(typeId, default(IList<SimpleActionModel>));

                if (data is not null)
                    result.AddRange(data);
            }
        }
        finally
        {
            semaphoreSlim.Release();
        }

        return result;
    }

    public async Task<RacesModel> GetRacesForActionAsync(Guid actionId)
    {
        var result = null as RacesModel;

        await semaphoreSlim.WaitAsync();
        try
        {
            result = _races.GetValueOrDefault(actionId, default(RacesModel));
        }
        finally
        {
            semaphoreSlim.Release();
        }

        if (result != null)
        {
            return result;
        }

        await LoadAndParseActionAsync(actionId, false, CancellationToken.None);


        await semaphoreSlim.WaitAsync();
        try
        {
            result = _races.GetValueOrDefault(actionId, default(RacesModel));
        }
        finally
        {
            semaphoreSlim.Release();
        }


        return result;
    }

    public async Task<RaceModel> GetRaceForActionAsync(Guid actionId, Guid raceId, CancellationToken cancellationToken)
    {
        var result = null as RaceModel;

        await semaphoreSlim.WaitAsync();
        try
        {
            result = _race.GetValueOrDefault((actionId, raceId), default(RaceModel));
        }
        finally
        {
            semaphoreSlim.Release();
        }

        if (result != null) 
        { 
            return result;
        }


        await LoadAndParseActionAsync(actionId, false, CancellationToken.None);

        await semaphoreSlim.WaitAsync();
        try
        {
            result = _race.GetValueOrDefault((actionId, raceId), default(RaceModel));
        }
        finally
        {
            semaphoreSlim.Release();
        }

        return result;
    }

    public async Task<CategoriesModel> GetCategoriesForActionRaceAsync(Guid actionId, Guid raceId)
    {
        var result = null as CategoriesModel;

        await semaphoreSlim.WaitAsync();
        try
        {
            result = _categories.GetValueOrDefault((actionId, raceId), default(CategoriesModel));
        }
        finally
        {
            semaphoreSlim.Release();
        }

        if (result == null)
        {
            await LoadAndParseActionAsync(actionId, false, CancellationToken.None);
        }

        await semaphoreSlim.WaitAsync();
        try
        {
            result = _categories.GetValueOrDefault((actionId, raceId), default(CategoriesModel));
        }
        finally
        {
            semaphoreSlim.Release();
        }

        return result;
    }

    public async Task<ResultsModel> GetResultsForActionRaceCategoryAsync(Guid actionId, Guid raceId, Guid categoryId, bool forceReloadFromServer)
    {
        var result = null as ResultsModel;

        if (forceReloadFromServer == false)
        {
            await semaphoreSlim.WaitAsync();

            try
            {
                result = _results.GetValueOrDefault((actionId, raceId, categoryId), default(ResultsModel));
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        if (result == null)
        {
            await LoadAndParseActionAsync(actionId, forceReloadFromServer, CancellationToken.None);
        }

        await semaphoreSlim.WaitAsync();

        try
        {
            result = _results.GetValueOrDefault((actionId, raceId, categoryId), default(ResultsModel));
        }
        finally
        {
            semaphoreSlim.Release();
        }

        return result;
    }

    public async Task<ResultsModel> GetResultsForActionRaceAsync(Guid actionId, Guid raceId, bool forceReloadFromServer)
    {
        var categories = await GetCategoriesForActionRaceAsync(actionId, raceId);

        var result = new ResultsModel
        {
            RaceId = raceId,
            ActionId = actionId,
            SynchronizedAt = categories.SynchronizedAt
        };

        foreach (var category in categories.Categories)
        {
            var ctg = await GetResultsForActionRaceCategoryAsync(actionId, raceId, category.Id, forceReloadFromServer);
            result.Results.AddRange(ctg.Results);
        }

        return result;
    }

    public async Task<bool> CanIEditResultsAsync(Guid actionId, CancellationToken cancellationToken)
    {
        var action = await _dataStorageServicePublicActions.GetAsync(actionId, false, cancellationToken);

        if (action != null)
        {
            var actionData = action.Data.Actions.FirstOrDefault(action => action.Id == actionId);
            
            if (actionData != null)
            {
                var requiredRoles = actionData.ResultsCanEdit;

                var myRoles = await GetMyRolesAsync();
                Console.WriteLine($"requiredRole: '{requiredRoles.Dump()}', having roles: '{myRoles.Dump()}'");

                if (myRoles.Any(role => requiredRoles.Contains(role)))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public async Task<ActionModel> GetActionModelAsync(Guid actionId, CancellationToken cancellationToken)
    {
        return null;
    }

    public async Task<SimpleActionModel> GetSimpleActionModelAsync(Guid actionId, CancellationToken cancellationToken)
    {
        var result = null as SimpleActionModel;

        await semaphoreSlim.WaitAsync();


        Console.WriteLine($"_actionsSimple: {_actionsSimple.Dump()}");

        try
        {
            result = _actionsSimple
                        .FirstOrDefault(it => it.Value.Any(i => i.Id == actionId))
                        .Value?.FirstOrDefault(result => result.Id == actionId);
        }
        finally
        {
            semaphoreSlim.Release();
        }

        if (result == null)
        {
            // TODO: now get all known; should be optimized by user settings
            await LoadAndParseActionsSimpleAsync(Constants.ActivityType.All, CancellationToken.None);
        }

        await semaphoreSlim.WaitAsync();

        try
        {
            result = _actionsSimple
                        .FirstOrDefault(it => it.Value.Any(i => i.Id == actionId))
                        .Value?.FirstOrDefault(result => result.Id == actionId);
        }
        finally
        {
            semaphoreSlim.Release();
        }

        return result;
    }

    public async Task AddResultAsync(Guid actionId, Guid raceId, Guid categoryId, ResultsModel.ResultDto result)
    {
        Protos.Actions.AddNewResult.RaceState state = Protos.Actions.AddNewResult.RaceState.NotSpecified;
        switch (result.State)
        {
            case ResultsModel.ResultState.NotValid:
                state = Protos.Actions.AddNewResult.RaceState.NotSpecified;
                break;

            case ResultsModel.ResultState.NotStarted:
                state = Protos.Actions.AddNewResult.RaceState.NotStarted;
                break;

            case ResultsModel.ResultState.Started:
                state = Protos.Actions.AddNewResult.RaceState.Started;
                break;

            case ResultsModel.ResultState.Finished:
                state = Protos.Actions.AddNewResult.RaceState.Finished;
                break;

            case ResultsModel.ResultState.DidNotFinished:
                state = Protos.Actions.AddNewResult.RaceState.DidNotFinished;
                break;

            case ResultsModel.ResultState.Disqualified:
                state = Protos.Actions.AddNewResult.RaceState.Disqualified;
                break;

            default:
                state = Protos.Actions.AddNewResult.RaceState.NotSpecified;
                break;

        }

        await ActionsClientInstance.addNewResultAsync(new Protos.Actions.AddNewResult.AddNewResultRequest
        {
            Id = result.Id != Guid.Empty ? result.Id.ToString() : Guid.NewGuid().ToString(),
            ActionId = actionId.ToString(),
            RaceId = raceId.ToString(),
            CategoryId = categoryId.ToString(),
            FirstName = result.FirstName,
            LastName = result.LastName,
            Pets = { result.Pets },
            State = state,
            Start = result.Start?.ToUniversalTime().ToGoogleTimestamp(),
            Finish = result.Finish?.ToUniversalTime().ToGoogleTimestamp(),
            Checkpoints = 
            { 
                result.Checkpoints
                        .Where(checkpoint => checkpoint.Time != null)
                        .Select(checkpoint => new Protos.Actions.AddNewResult.Checkpointdto
                                {
                                    Id = checkpoint.Id.ToString(),
                                    Time = checkpoint.Time?.ToUniversalTime().ToGoogleTimestamp()
                                })
                        .ToList() 
            }
        });
    }

    public async Task StartNow(Guid actionId, Guid raceId, Guid categoryId, Guid racerId)
    {
        await ActionsClientInstance.startNowAsync(new Protos.Actions.StartNow.StartNowRequest
        {
            ActionId = actionId.ToString(),
            RaceId = raceId.ToString(),
            CategoryId = categoryId.ToString(),
            RacerId = racerId.ToString(),
            Time = DateTimeOffset.UtcNow.ToGoogleTimestamp()
        });
    }

    public async Task FinishNow(Guid actionId, Guid raceId, Guid categoryId, Guid racerId)
    {
        await ActionsClientInstance.finishNowAsync(new Protos.Actions.FinishNow.FinishNowRequest
        {
            ActionId = actionId.ToString(),
            RaceId = raceId.ToString(),
            CategoryId = categoryId.ToString(),
            RacerId = racerId.ToString(),
            Time = DateTimeOffset.UtcNow.ToGoogleTimestamp()
        });
    }

    public async Task DeleteStart(Guid actionId, Guid raceId, Guid categoryId, Guid racerId)
    {
        await ActionsClientInstance.deleteStartAsync(new Protos.Actions.DeleteStart.DeleteStartRequest
        {
            ActionId = actionId.ToString(),
            RaceId = raceId.ToString(),
            CategoryId = categoryId.ToString(),
            RacerId = racerId.ToString()
        });
    }

    public async Task DeleteFinish(Guid actionId, Guid raceId, Guid categoryId, Guid racerId)
    {
        await ActionsClientInstance.deleteFinishAsync(new Protos.Actions.DeleteFinish.DeleteFinishRequest
        {
            ActionId = actionId.ToString(),
            RaceId = raceId.ToString(),
            CategoryId = categoryId.ToString(),
            RacerId = racerId.ToString()
        });
    }

    public async Task Dns(Guid actionId, Guid raceId, Guid categoryId, Guid racerId)
    {
        await ActionsClientInstance.dnsAsync(new Protos.Actions.Dns.DnsRequest
        {
            ActionId = actionId.ToString(),
            RaceId = raceId.ToString(),
            CategoryId = categoryId.ToString(),
            RacerId = racerId.ToString()
        });
    }

    public async Task Dnf(Guid actionId, Guid raceId, Guid categoryId, Guid racerId)
    {
        await ActionsClientInstance.dnfAsync(new Protos.Actions.Dnf.DnfRequest
        {
            ActionId = actionId.ToString(),
            RaceId = raceId.ToString(),
            CategoryId = categoryId.ToString(),
            RacerId = racerId.ToString()
        });
    }

    public async Task Dsq(Guid actionId, Guid raceId, Guid categoryId, Guid racerId)
    {
        await ActionsClientInstance.dsqAsync(new Protos.Actions.Dsq.DsqRequest
        {
            ActionId = actionId.ToString(),
            RaceId = raceId.ToString(),
            CategoryId = categoryId.ToString(),
            RacerId = racerId.ToString()
        });
    }

    public async Task ResetStates(Guid actionId, Guid raceId, Guid categoryId, Guid racerId)
    {
        await ActionsClientInstance.resetStatesAsync(new Protos.Actions.ResetStates.ResetStatesRequest
        {
            ActionId = actionId.ToString(),
            RaceId = raceId.ToString(),
            CategoryId = categoryId.ToString(),
            RacerId = racerId.ToString()
        });
    }

    private async Task LoadAndParseActionAsync(Guid actionId, bool forceReloadFromServer, CancellationToken cancellationToken)
    {
        var action = await _dataStorageServicePublicActions.GetAsync(actionId, forceReloadFromServer, cancellationToken);

        if (action != null)
        {
            await semaphoreSlim.WaitAsync();

            try
            {
                _races[actionId] = _mapper.Map<RacesModel>(action);

                foreach (var race in action.Data.Actions[0].Races)
                {
                    var raceId = race.Id;

                    var checkpointsInRace = race.Checkpoints;

                    _categories[(actionId, raceId)] = _mapper.Map<CategoriesModel>(race) with
                    {
                        ActionId = action.Data.Actions[0].Id,
                        SynchronizedAt = action.Created
                    };

                    foreach (var category in race.Categories)
                    {
                        _results[(actionId, raceId, category.Id)] = _mapper.Map<ResultsModel>(category) with
                        {
                            SynchronizedAt = action.Created
                        };
                    }

                    _race[(actionId, raceId)] = _mapper.Map<RaceModel>(race) with
                    {
                        SynchronizedAt = action.Created,
                        ActionId = actionId,
                        RaceId = raceId
                    };
                }
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }
    }

    private async Task LoadAndParseActionsSimpleAsync(IList<Guid> typeIds, CancellationToken cancellationToken)
    {
        var actionsInDataStorage = await _dataStorageServiceActionsByType.GetListAsync(typeIds, false,cancellationToken);

        if (_actionsSimple != null && actionsInDataStorage != null)
        {
            await semaphoreSlim.WaitAsync();

            try
            {
                foreach (var action in actionsInDataStorage.Data.Actions)
                {
                    if (_actionsSimple.TryGetValue(action.TypeId, out var tmp) == false)
                        _actionsSimple[action.TypeId] = new List<SimpleActionModel>();

                    _actionsSimple[action.TypeId].Add(new SimpleActionModel
                    {
                        Id = action.Id,
                        Name = action.Name,
                        Description = action.Description,
                        TypeId = action.TypeId,
                        Begin = action.Begin,
                        End = action.End,
                        City = action.City,
                        Races = action.Races.Select(race => new SimpleActionModel.RaceDto { Id = race.Id, Name = race.Name}).ToList()
                    });
                }
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }
    }
}
