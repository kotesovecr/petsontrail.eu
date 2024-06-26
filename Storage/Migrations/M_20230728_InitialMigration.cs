﻿using Storage.Entities.ActionRights;
using Storage.Entities.AuthorizationRoles;
using Storage.Entities.UserProfiles;
using Storage.Migrations._2024;
using Storage.Services.Repositories.Actions;

namespace Storage.Migrations;

internal class M_20230728_InitialMigration : M_00_MigrationBase
{
    protected override Guid Id { get; init; } = Guid.Parse("6362aefb-e6fc-4bd1-a534-26c5b7333cff");
    protected override string Name { get; init; } = nameof(M_20230728_InitialMigration);


    private Guid _userIdAdmin => Guid.Parse("29bf10f1-8f4a-43f3-8e1b-448d0fbf7bef");
    private Guid _userIdRadekKotesovec => Guid.Parse("fdf633bc-91d0-4f2d-ab78-f01657eca190");

    public M_20230728_InitialMigration(IServiceProvider serviceProvider) : base(serviceProvider) 
    {
        this
            .AddUpAction(() => AuthorizationRolesRepositoryService.AddAuthorizationRoleAsync(new AddAuthorizationRoleRequest
            {
                Id = Constants.Roles.InternalAdministrator.GUID,
                UserId = _userIdAdmin,
                Name = Constants.Roles.InternalAdministrator.Name,
                Actions = new List<Constants.ActionType>
                {
                    Constants.ActionType.GlobalRead,
                    Constants.ActionType.GlobalInsert,
                    Constants.ActionType.GlobalUpdate,
                    Constants.ActionType.GlobalDelete
                }
            }, CancellationToken.None))

            .AddUpAction(() => AuthorizationRolesRepositoryService.AddAuthorizationRoleAsync(new AddAuthorizationRoleRequest
            {
                Id = Constants.Roles.InternalUser.GUID,
                UserId = _userIdAdmin,
                Name = Constants.Roles.InternalUser.Name,
                Actions = new List<Constants.ActionType>
                {
                    Constants.ActionType.ActionsRead,
                    Constants.ActionType.ActionsInsert,
                    Constants.ActionType.ActionsUpdateOwn,
                    Constants.ActionType.ActionsDeleteOwn
                }
            }, CancellationToken.None))

            .AddUpAction(() => AuthorizationRolesRepositoryService.AddAuthorizationRoleAsync(new AddAuthorizationRoleRequest
            {
                Id = Constants.Roles.ExternalUser.GUID,
                UserId = _userIdAdmin,
                Name = Constants.Roles.ExternalUser.Name,
                Actions = new List<Constants.ActionType>
                {
                    Constants.ActionType.ActionsRead
                }
            }, CancellationToken.None))

            //------------------------------------------------------------------------------------------------------------

            .AddUpAction(() => UserProfilesRepositoryService.AddUserProfileAsync(new CreateUserProfileInternalStorageRequest
            {
                Id = _userIdAdmin,
                UserId = _userIdAdmin,
                FirstName = "Admin",
                LastName = "Admin",
                Contact = new CreateUserProfileInternalStorageRequest.ContactDto
                {
                    EmailAddress = "admin@petsontrail.eu",
                    PhoneNumber = "+420 728 245 996"
                },
                Email = "admin@petsontrail.eu",
                Roles = new List<Guid>
                {
                    Constants.Roles.InternalAdministrator.GUID
                }
            }, CancellationToken.None))

            .AddUpAction(() => UserProfilesRepositoryService.AddUserProfileAsync(new CreateUserProfileInternalStorageRequest
            {
                Id = _userIdRadekKotesovec,
                UserId = _userIdRadekKotesovec,
                FirstName = "Radek",
                LastName = "Kotěšovec",
                Contact = new CreateUserProfileInternalStorageRequest.ContactDto
                {
                    EmailAddress = "radek.kotesovec@dogtrekking.cz",
                    PhoneNumber = "+420 728 245 996"
                },
                Email = "radek.kotesovec@dogtrekking.cz",
                Roles = new List<Guid>
                {
                    Constants.Roles.InternalAdministrator.GUID
                }
            }, CancellationToken.None))

            // ------------------------------------------------------------------------------------------------------------

            .AddUpAction(() => ActivityTypeRepositoryService.AddAsync(new Entities.ActivityTypes.AddActivityTypeInternalStorageRequest
            {
                Id = Constants.ActivityType.Dogtrekking,
                Name = "Dogtrekking",
                Description = "Dogtrekking",
                AdapterPrefix = "dogtrekking",
                IconPath = "/icons/activity_types/dogtrekking.png",
                TimeTracking = true,
                DistanceTracking = false,
                SpeedTracking = false,
                PointTracking = false
            }, CancellationToken.None))

            .AddUpAction(() => ActivityTypeRepositoryService.AddAsync(new Entities.ActivityTypes.AddActivityTypeInternalStorageRequest
            {
                Id = Constants.ActivityType.Obedience,
                Name = "Obedience",
                Description = "Obedience",
                AdapterPrefix = "obedience",
                IconPath = "/icons/activity_types/obedience.png",
                TimeTracking = true,
                DistanceTracking = false,
                SpeedTracking = false,
                PointTracking = true
            }, CancellationToken.None))

            .AddUpAction(() => ActivityTypeRepositoryService.AddAsync(new Entities.ActivityTypes.AddActivityTypeInternalStorageRequest
            {
                Id = Constants.ActivityType.RallyObedience,
                Name = "Rally Obedience",
                Description = "Rally Obedience",
                AdapterPrefix = "rallyobedience",
                IconPath = "/icons/activity_types/rallyobedience.png",
                TimeTracking = true,
                DistanceTracking = false,
                SpeedTracking = false,
                PointTracking = true
            }, CancellationToken.None))

            .AddUpAction(() => ActivityTypeRepositoryService.AddAsync(new Entities.ActivityTypes.AddActivityTypeInternalStorageRequest
            {
                Id = Constants.ActivityType.Agility,
                Name = "Agility",
                Description = "Agility",
                AdapterPrefix = "agility",
                IconPath = "/icons/activity_types/agility.png",
                TimeTracking = true,
                DistanceTracking = false,
                SpeedTracking = false,
                PointTracking = true
            }, CancellationToken.None))

            .AddUpAction(() => ActivityTypeRepositoryService.AddAsync(new Entities.ActivityTypes.AddActivityTypeInternalStorageRequest
            {
                Id = Constants.ActivityType.Canicross,
                Name = "Canicross",
                Description = "Canicross",
                AdapterPrefix = "canicross",
                IconPath = "/icons/activity_types/canicross.png",
                TimeTracking = true,
                DistanceTracking = true,
                SpeedTracking = true,
                PointTracking = false
            }, CancellationToken.None))

            .AddUpAction(() => ActivityTypeRepositoryService.AddAsync(new Entities.ActivityTypes.AddActivityTypeInternalStorageRequest
            {
                Id = Constants.ActivityType.Bikejoring,
                Name = "Bikejoring",
                Description = "Bikejoring",
                AdapterPrefix = "bikejoring",
                IconPath = "/icons/activity_types/bikejoring.png",
                TimeTracking = true,
                DistanceTracking = true,
                SpeedTracking = true,
                PointTracking = false
            }, CancellationToken.None))

            .AddUpAction(() => ActivityTypeRepositoryService.AddAsync(new Entities.ActivityTypes.AddActivityTypeInternalStorageRequest
            {
                Id = Constants.ActivityType.Skijoring,
                Name = "Skijoring",
                Description = "Skijoring",
                AdapterPrefix = "skijoring",
                IconPath = "/icons/activity_types/skijoring.png",
                TimeTracking = true,
                DistanceTracking = true,
                SpeedTracking = true,
                PointTracking = false
            }, CancellationToken.None))

            .AddUpAction(() => ActivityTypeRepositoryService.AddAsync(new Entities.ActivityTypes.AddActivityTypeInternalStorageRequest
            {
                Id = Constants.ActivityType.SledDog,
                Name = "Sled Dog",
                Description = "Sled Dog",
                AdapterPrefix = "sleddog",
                IconPath = "/icons/activity_types/sleddog.png",
                TimeTracking = true,
                DistanceTracking = true,
                SpeedTracking = true,
                PointTracking = false
            }, CancellationToken.None))

            .AddUpAction(() => ActivityTypeRepositoryService.AddAsync(new Entities.ActivityTypes.AddActivityTypeInternalStorageRequest
            {
                Id = Constants.ActivityType.DogDancing,
                Name = "Dog Dancing",
                Description = "Dog Dancing",
                AdapterPrefix = "dogdancing",
                IconPath = "/icons/activity_types/dogdancing.png",
                TimeTracking = true,
                DistanceTracking = false,
                SpeedTracking = false,
                PointTracking = false
            }, CancellationToken.None))

            .AddUpAction(() => ActivityTypeRepositoryService.AddAsync(new Entities.ActivityTypes.AddActivityTypeInternalStorageRequest
            {
                Id = Constants.ActivityType.Frisbee,
                Name = "Frisbee",
                Description = "Frisbee",
                AdapterPrefix = "frisbee",
                IconPath = "/icons/activity_types/frisbee.png",
                TimeTracking = true,
                DistanceTracking = false,
                SpeedTracking = false,
                PointTracking = false
            }, CancellationToken.None))

            .AddUpAction(() => ActivityTypeRepositoryService.AddAsync(new Entities.ActivityTypes.AddActivityTypeInternalStorageRequest
            {
                Id = Constants.ActivityType.Flyball,
                Name = "Flyball",
                Description = "Flyball",
                AdapterPrefix = "flyball",
                IconPath = "/icons/activity_types/flyball.png",
                TimeTracking = true,
                DistanceTracking = false,
                SpeedTracking = false,
                PointTracking = false
            }, CancellationToken.None))

            .AddUpAction(() => ActivityTypeRepositoryService.AddAsync(new Entities.ActivityTypes.AddActivityTypeInternalStorageRequest
            {
                Id = Constants.ActivityType.DiscDog,
                Name = "Disc Dog",
                Description = "Disc Dog",
                AdapterPrefix = "discdog",
                IconPath = "/icons/activity_types/discdog.png",
                TimeTracking = true,
                DistanceTracking = false,
                SpeedTracking = false,
                PointTracking = false
            }, CancellationToken.None))

            .AddUpAction(() => ActivityTypeRepositoryService.AddAsync(new Entities.ActivityTypes.AddActivityTypeInternalStorageRequest
            {
                Id = Constants.ActivityType.DogHiking,
                Name = "Dog Hiking",
                Description = "Dog Hiking",
                AdapterPrefix = "doghiking",
                IconPath = "/icons/activity_types/doghiking.png",
                TimeTracking = true,
                DistanceTracking = true,
                SpeedTracking = true,
                PointTracking = false
            }, CancellationToken.None))

            .AddUpAction(() => ActivityTypeRepositoryService.AddAsync(new Entities.ActivityTypes.AddActivityTypeInternalStorageRequest
            {
                Id = Constants.ActivityType.Hiking,
                Name = "Hiking",
                Description = "Hiking without pets",
                AdapterPrefix = "hiking",
                IconPath = "/icons/activity_types/hiking.png",
                TimeTracking = true,
                DistanceTracking = true,
                SpeedTracking = true,
                PointTracking = false
            }, CancellationToken.None))

            .AddUpAction(() => ActivityTypeRepositoryService.AddAsync(new Entities.ActivityTypes.AddActivityTypeInternalStorageRequest
            {
                Id = Constants.ActivityType.Biking,
                Name = "Biking",
                Description = "Biking without pets",
                AdapterPrefix = "biking",
                IconPath = "/icons/activity_types/biking.png",
                TimeTracking = true,
                DistanceTracking = true,
                SpeedTracking = true,
                PointTracking = false
            }, CancellationToken.None))

            .AddUpAction(() => ActivityTypeRepositoryService.AddAsync(new Entities.ActivityTypes.AddActivityTypeInternalStorageRequest
            {
                Id = Constants.ActivityType.Running,
                Name = "Running",
                Description = "Running without pets",
                AdapterPrefix = "running",
                IconPath = "/icons/activity_types/running.png",
                TimeTracking = true,
                DistanceTracking = true,
                SpeedTracking = true,
                PointTracking = false
            }, CancellationToken.None))

            .AddUpAction(() => ActivityTypeRepositoryService.AddAsync(new Entities.ActivityTypes.AddActivityTypeInternalStorageRequest
            {
                Id = Constants.ActivityType.Walking,
                Name = "Walking",
                Description = "Walking without pets",
                AdapterPrefix = "walking",
                IconPath = "/icons/activity_types/walking.png",
                TimeTracking = true,
                DistanceTracking = true,
                SpeedTracking = true,
                PointTracking = false
            }, CancellationToken.None))

            .AddUpAction(() => ActivityTypeRepositoryService.AddAsync(new Entities.ActivityTypes.AddActivityTypeInternalStorageRequest
            {
                Id = Constants.ActivityType.Skating,
                Name = "Skating",
                Description = "Skating without pets",
                AdapterPrefix = "skating",
                IconPath = "/icons/activity_types/skating.png",
                TimeTracking = true,
                DistanceTracking = true,
                SpeedTracking = true,
                PointTracking = false
            }, CancellationToken.None))

            .AddUpAction(() => ActivityTypeRepositoryService.AddAsync(new Entities.ActivityTypes.AddActivityTypeInternalStorageRequest
            {
                Id = Constants.ActivityType.Skiing,
                Name = "Skiing",
                Description = "Skiing without pets",
                AdapterPrefix = "skiing",
                IconPath = "/icons/activity_types/skiing.png",
                TimeTracking = true,
                DistanceTracking = true,
                SpeedTracking = true,
                PointTracking = false
            }, CancellationToken.None))

            .AddUpAction(() => ActivityTypeRepositoryService.AddAsync(new Entities.ActivityTypes.AddActivityTypeInternalStorageRequest
            {
                Id = Constants.ActivityType.Snowboarding,
                Name = "Snowboarding",
                Description = "Snowboarding without pets",
                AdapterPrefix = "snowboarding",
                IconPath = "/icons/activity_types/snowboarding.png",
                TimeTracking = true,
                DistanceTracking = true,
                SpeedTracking = true,
                PointTracking = false
            }, CancellationToken.None))

            .AddUpAction(() => ActivityTypeRepositoryService.AddAsync(new Entities.ActivityTypes.AddActivityTypeInternalStorageRequest
            {
                Id = Constants.ActivityType.EScooter,
                Name = "EScootering",
                Description = "EScootering",
                AdapterPrefix = "escootering",
                IconPath = "/icons/activity_types/escootering.png",
                TimeTracking = true,
                DistanceTracking = true,
                SpeedTracking = true,
                PointTracking = false
            }, CancellationToken.None))

            // ------------------------------------------------------------------------------------------------------------

            .AddUpAction(() => VaccinationTypeRepositoryService.AddAsync(new Entities.VaccinationTypes.AddVaccinationTypeInternalStorageRequest
            {
                Id = Constants.VaccinationType.Rabies,
                UserId = _userIdAdmin,
                Name = "Rabies",
                Description = "Rabies"
            }, CancellationToken.None))

            // ------------------------------------------------------------------------------------------------------------

            .AddUpAction(() => PetTypeRepositoryService.AddAsync(new Entities.PetTypes.AddPetTypeInternalStorageRequest
            {
                Id = Constants.PetType.Dog,
                UserId = _userIdAdmin,
                Name = "Dog",
                Description = "Dog"
            }, CancellationToken.None))

            .AddUpAction(() => PetTypeRepositoryService.AddAsync(new Entities.PetTypes.AddPetTypeInternalStorageRequest
            {
                Id = Constants.PetType.Cat,
                UserId = _userIdAdmin,
                Name = "Cat",
                Description = "Cat"
            }, CancellationToken.None))

            .AddUpAction(() => PetTypeRepositoryService.AddAsync(new Entities.PetTypes.AddPetTypeInternalStorageRequest
            {
                Id = Constants.PetType.Horse,
                UserId = _userIdAdmin,
                Name = "Horse",
                Description = "Horse"
            }, CancellationToken.None))

            // ------------------------------------------------------------------------------------------------------------
            // ------------------------------------------------------------------------------------------------------------
            // ------------------------------------------------------------------------------------------------------------

            .AddDownAction(() => UserProfilesRepositoryService.DeleteUserProfileAsync(new DeleteUserProfileInternalStorageRequest { Id = _userIdRadekKotesovec }, CancellationToken.None))
            .AddDownAction(() => UserProfilesRepositoryService.DeleteUserProfileAsync(new DeleteUserProfileInternalStorageRequest { Id = _userIdAdmin }, CancellationToken.None))
            .AddDownAction(() => AuthorizationRolesRepositoryService.DeleteAuthorizationRoleAsync(Constants.Roles.InternalAdministrator.GUID, CancellationToken.None))
            .AddDownAction(() => AuthorizationRolesRepositoryService.DeleteAuthorizationRoleAsync(Constants.Roles.InternalUser.GUID, CancellationToken.None))
            .AddDownAction(() => AuthorizationRolesRepositoryService.DeleteAuthorizationRoleAsync(Constants.Roles.ExternalUser.GUID, CancellationToken.None))
            .AddDownAction(() => ActivityTypeRepositoryService.DeleteAsync(Constants.ActivityType.Dogtrekking, CancellationToken.None))
            .AddDownAction(() => ActivityTypeRepositoryService.DeleteAsync(Constants.ActivityType.Obedience, CancellationToken.None))
            .AddDownAction(() => ActivityTypeRepositoryService.DeleteAsync(Constants.ActivityType.RallyObedience, CancellationToken.None))
            .AddDownAction(() => ActivityTypeRepositoryService.DeleteAsync(Constants.ActivityType.Agility, CancellationToken.None))
            .AddDownAction(() => ActivityTypeRepositoryService.DeleteAsync(Constants.ActivityType.Canicross, CancellationToken.None))
            .AddDownAction(() => ActivityTypeRepositoryService.DeleteAsync(Constants.ActivityType.Bikejoring, CancellationToken.None))
            .AddDownAction(() => ActivityTypeRepositoryService.DeleteAsync(Constants.ActivityType.Skijoring, CancellationToken.None))
            .AddDownAction(() => ActivityTypeRepositoryService.DeleteAsync(Constants.ActivityType.SledDog, CancellationToken.None))
            .AddDownAction(() => ActivityTypeRepositoryService.DeleteAsync(Constants.ActivityType.DogDancing, CancellationToken.None))
            .AddDownAction(() => ActivityTypeRepositoryService.DeleteAsync(Constants.ActivityType.Frisbee, CancellationToken.None))
            .AddDownAction(() => ActivityTypeRepositoryService.DeleteAsync(Constants.ActivityType.Flyball, CancellationToken.None))
            .AddDownAction(() => ActivityTypeRepositoryService.DeleteAsync(Constants.ActivityType.DiscDog, CancellationToken.None))
            .AddDownAction(() => ActivityTypeRepositoryService.DeleteAsync(Constants.ActivityType.DogHiking, CancellationToken.None))
            .AddDownAction(() => ActivityTypeRepositoryService.DeleteAsync(Constants.ActivityType.Hiking, CancellationToken.None))
            .AddDownAction(() => ActivityTypeRepositoryService.DeleteAsync(Constants.ActivityType.Biking, CancellationToken.None))
            .AddDownAction(() => ActivityTypeRepositoryService.DeleteAsync(Constants.ActivityType.Running, CancellationToken.None))
            .AddDownAction(() => ActivityTypeRepositoryService.DeleteAsync(Constants.ActivityType.Walking, CancellationToken.None))
            .AddDownAction(() => ActivityTypeRepositoryService.DeleteAsync(Constants.ActivityType.Skating, CancellationToken.None))
            .AddDownAction(() => ActivityTypeRepositoryService.DeleteAsync(Constants.ActivityType.Skiing, CancellationToken.None))
            .AddDownAction(() => ActivityTypeRepositoryService.DeleteAsync(Constants.ActivityType.Snowboarding, CancellationToken.None))
            .AddDownAction(() => ActivityTypeRepositoryService.DeleteAsync(Constants.ActivityType.EScooter, CancellationToken.None))
            .AddDownAction(() => VaccinationTypeRepositoryService.DeleteAsync(Constants.VaccinationType.Rabies, CancellationToken.None))
            .AddDownAction(() => PetTypeRepositoryService.DeleteAsync(Constants.PetType.Dog, CancellationToken.None))
            .AddDownAction(() => PetTypeRepositoryService.DeleteAsync(Constants.PetType.Cat, CancellationToken.None))
            .AddDownAction(() => PetTypeRepositoryService.DeleteAsync(Constants.PetType.Horse, CancellationToken.None));
    }
}