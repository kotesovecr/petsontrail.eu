﻿namespace PetsOnTrail.Interfaces.Actions.Entities.Results;

public sealed record AddResultRequest
{
    public string ActionId { get; set; } = string.Empty;

    public string RaceId { get; set; } = string.Empty;

    public string CategoryId { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public FinalState State { get; set; } = FinalState.Finished;

    public DateTimeOffset? Start { get; set; } = null;

    public DateTimeOffset? Finish { get; set; } = null;

    public List<string> Pets {  get; set; } = new List<string>();
    
    public enum FinalState {
        NotSpecified = 0,
        Accepted = 1,
        Started = 2,
        Finished = 3,
        DNF = 4,
        DNS = 5,
        DSQ = 6
    }
}

