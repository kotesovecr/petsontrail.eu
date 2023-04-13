using Grpc.Core;
using MapsterMapper;
using Protos.Shared;
using Storage.Entities.UserProfiles;
using Storage.Interfaces;
using GetUserProfileRequest = Storage.Entities.UserProfiles.GetUserProfileRequest;
using GetUserProfileResponse = Protos.UserProfiles.GetUserProfileResponse;
using UpdateUserProfileRequest = Storage.Entities.UserProfiles.UpdateUserProfileRequest;

namespace DogtrekkingCzGRPCService.Services.UserProfiles;

public class UserProfilesService : Protos.UserProfiles.UserProfiles.UserProfilesBase
{
    private readonly ILogger<UserProfilesService> _logger;
    private readonly IMapper _mapper;
    private readonly IUserProfilesRepositoryService _userProfilesRepositoryService;

    public UserProfilesService(ILogger<UserProfilesService> logger, IMapper mapper, IUserProfilesRepositoryService userProfilesRepositoryService)
    {
        _logger = logger;
        _mapper = mapper;
        _userProfilesRepositoryService = userProfilesRepositoryService;
    }
    
    public async override Task<Protos.UserProfiles.GetUserProfileResponse> getUserProfile(Protos.UserProfiles.GetUserProfileRequest request, ServerCallContext context)
    {
        var getUserProfileRequest = _mapper.Map<GetUserProfileRequest>(request);
        var getUserProfileResponse = await _userProfilesRepositoryService.GetUserProfileAsync(getUserProfileRequest, context.CancellationToken);

        if (getUserProfileResponse == null)
            return new GetUserProfileResponse
            {
                UserProfile = new UserProfile
                {
                    Id = ""                    
                }
            };

        var response = new GetUserProfileResponse
        {
            UserProfile = _mapper.Map<UserProfile>(getUserProfileResponse)
        };

        return response;
    }

    public async override Task<Protos.UserProfiles.CreateUserProfileResponse> registerUserProfile(Protos.UserProfiles.CreateUserProfileRequest request, ServerCallContext context)
    {
        var addUserProfileRequest = _mapper.Map<AddUserProfileRequest>(request.UserProfile);
        var addUserProfileResponse = await _userProfilesRepositoryService.AddUserProfileAsync(addUserProfileRequest, context.CancellationToken);

        var response = _mapper.Map<Protos.UserProfiles.CreateUserProfileResponse>(addUserProfileResponse);

        return response;
    }
    
    public async override Task<Protos.UserProfiles.UpdateUserProfileResponse> updateUserProfile(Protos.UserProfiles.UpdateUserProfileRequest request, ServerCallContext context)
    {
        var updateUserProfileRequest = _mapper.Map<UpdateUserProfileRequest>(request.UserProfile);
        var updateUserProfileResponse = await _userProfilesRepositoryService.UpdateUserProfileAsync(updateUserProfileRequest, context.CancellationToken);

        var response = _mapper.Map<Protos.UserProfiles.UpdateUserProfileResponse>(updateUserProfileResponse);

        return response;
    }

}