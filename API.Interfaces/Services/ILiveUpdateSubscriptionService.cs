﻿using System.Collections.ObjectModel;
using DogsOnTrail.Interfaces.Actions.Entities.LiveUpdateSubscription;

namespace DogsOnTrail.Interfaces.Actions.Services;

public interface ILiveUpdateSubscriptionService
{
    IDictionary<string, LiveUpdateSubscriptionData> Repository { get; set; }

    public Task SendAsync(SendLiveUpdateRequest request, CancellationToken cancellationToken);
    
    Task UserCancelledSubscriptionAsync(CancelLiveUpdateSubscriptionRequest request, CancellationToken cancellationToken);

    Task AddLiveUpdateSubscriptionAsync(AddLiveUpdateSubscriptionRequest request, CancellationToken cancellationToken);
}