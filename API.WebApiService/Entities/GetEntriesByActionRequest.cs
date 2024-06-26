﻿using Mediator;

namespace API.WebApiService.Entities;

public sealed record GetEntriesByActionRequest : IRequest<GetEntriesByActionResponse>
{
    public string ActionId { get; set; }

    public bool IncludeAlreadyAccepted { get; set; } = false;
}