﻿namespace Storage.Migrations._2024;

internal class _20240712_StopouStrejdySeraka : M_00_MigrationBase
{
    protected override Guid Id { get; init; } = Guid.Parse("854b2a95-7ee8-4b49-9da5-851928ce1822");
    protected override string Name { get; init; } = nameof(_20240712_StopouStrejdySeraka);

    public _20240712_StopouStrejdySeraka(IServiceProvider serviceProvider) : base(serviceProvider) 
    { 
        this
            .AddUpAction(() => ActionsRepositoryService.AddActionAsync(new Entities.Actions.CreateActionInternalStorageRequest
            {
                Id = Id,
                Name = "Stopou strejdy Šeráka",
                TypeId = Constants.ActivityType.Dogtrekking,
                ResultsCanEdit = new List<Guid> { Constants.Roles.InternalAdministrator.GUID },
                CompetitorsCanEdit = new List<Guid> { Constants.Roles.InternalAdministrator.GUID },
                ActionCanEdit = new List<Guid> { Constants.Roles.InternalAdministrator.GUID },
                Address = new Entities.Actions.CreateActionInternalStorageRequest.AddressDto
                {
                    City = "Lipová lázně"
                },
                Term = new Entities.Actions.CreateActionInternalStorageRequest.TermDto
                {
                    From = new DateTime(2024, 7, 12, 17, 0, 0),
                    To = new DateTime(2024, 7, 14, 13, 0, 0)
                }
            }, CancellationToken.None))

            .AddDownAction(() => ActionsRepositoryService.DeleteActionAsync(Id, CancellationToken.None));
    }
}
