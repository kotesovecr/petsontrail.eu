﻿namespace DogsOnTrail.Interfaces.Actions.Entities.Actions;

public sealed record GetActionEntrySettingsRequest
{
    public string ActionId { get; set; }
}