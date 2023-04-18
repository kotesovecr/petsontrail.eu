﻿using DogtrekkingCzShared.Entities;

namespace DogtrekkingCz.Interfaces.Actions.Entities.Actions;

public sealed record GetAllActionsWithDetailsResponse
{
    public IReadOnlyList<ActionDto> Actions { get; init; } = new List<ActionDto>();
}