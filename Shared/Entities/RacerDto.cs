﻿namespace DogtrekkingCz.Shared.Entities
{
    public enum RaceState
    {
        NotValid = 0,
        NotStarted,
        Started,
        Finished,
        DidNotFinished,
        Disqualified
    }

    public sealed record RacerDto
    {
        public string CompetitorId { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public List<string> Dogs { get; set; } = new List<string>();

        public DateTimeOffset? Start { get; set; } = null;

        public DateTimeOffset? Finish { get; set; } = null;

        public RaceState State { get; set; } = RaceState.NotValid;

        public List<NoteDto> Notes { get; set; } = new List<NoteDto>();
    }
}
