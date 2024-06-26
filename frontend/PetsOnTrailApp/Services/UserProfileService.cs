﻿using MapsterMapper;
using PetsOnTrailApp.Models;
using PetsOnTrailApp.Providers;
using Protos.UserProfiles.GetUserProfile;
using SharedLib.Models;

namespace PetsOnTrailApp.Services;

public sealed class UserProfileService : IUserProfileService
{
    private UserProfileModel _userProfileModel = new();

    private readonly Protos.ActionRights.ActionRights.ActionRightsClient _actionRightsClient;
    private readonly Protos.UserProfiles.UserProfiles.UserProfilesClient _userProfilesClient;
    private readonly IServiceProvider _serviceProvider;
    private readonly TokenStorage _tokenStorage;
    private readonly SharedLib.Providers.ITokenProvider _tokenProvider;

    private DateTimeOffset? IsValidTime { get; set; } = null;

    public UserProfileService(
        Protos.ActionRights.ActionRights.ActionRightsClient actionRightsClient, 
        Protos.UserProfiles.UserProfiles.UserProfilesClient userProfilesClient,
        TokenStorage tokenStorage,
        SharedLib.Providers.ITokenProvider tokenProvider,
        IServiceProvider serviceProvider)
    {
        _actionRightsClient = actionRightsClient;
        _userProfilesClient = userProfilesClient;
        _serviceProvider = serviceProvider;
        _tokenStorage = tokenStorage;
        _tokenProvider = tokenProvider;
    }

    public async Task<UserProfileModel> GetAsync()
    {
        var token = await _tokenStorage.GetAccessToken();
        if (string.IsNullOrWhiteSpace(token))
        {
            token = await _tokenProvider.GetTokenAsync();

            if (string.IsNullOrWhiteSpace(token))
                return null;

            await _tokenStorage.SetTokensAsync(token, "");
        }

        if (IsValidTime != null && IsValidTime > DateTimeOffset.Now.AddMinutes(-5) && _userProfileModel != null)
            return _userProfileModel;
                
        try
        {
            var userProfile = await _userProfilesClient.getUserProfileAsync(new GetUserProfileRequest());
            if (userProfile == null || userProfile.Id == string.Empty)
            {
                IsValidTime = DateTimeOffset.Now;
                return null;
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
                SetUserProfile(mapper.Map<UserProfileModel>(userProfile));
            }


            // TODO: Do I need this? The rights for actions are stored in the actions themselves as the list of required roles...
            //var getActionRightsResponse = await _actionRightsClient.getActionRightsAsync(
            //    new Protos.ActionRights.GetActionRights.GetActionRightsRequest
            //    {
            //        Id = ""
            //    });

            //var userRights = new List<UserProfileModel.ActionRightsDto>();
            //foreach (var right in getActionRightsResponse.Rights)
            //{
            //    userRights.Add(new UserProfileModel.ActionRightsDto
            //    {
            //        Id = Guid.Parse(right.Id),
            //        ActionId = Guid.Parse(right.ActionId),
            //        UserId = Guid.Parse(right.UserId),
            //        Roles = right.Roles.Select(role => Guid.Parse(role)).ToList()
            //    });
            //}

            //SetRights(userRights);

            IsValidTime = DateTimeOffset.Now;

            return _userProfileModel;
        }
        catch (Exception ex)
        {
            IsValidTime = DateTimeOffset.Now;
            return null;
        }
    }

    public void Invalidate()
    {
        IsValidTime = null;
    }

    public void SetUserProfile(UserProfileModel userProfileModel)
    {
        var rights = _userProfileModel.Rights;
        
        using (var scope = _serviceProvider.CreateScope())
        {
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
            
            _userProfileModel = mapper.Map<UserProfileModel>(userProfileModel)
                with
                {
                    Rights = rights
                };
        }
    }
    
    public void SetRights(IList<UserProfileModel.ActionRightsDto> rights)
    {
        _userProfileModel.Rights = rights;
    }
}