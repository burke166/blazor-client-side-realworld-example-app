using System.Threading.Tasks;
using BlazorClientSideRealWorld.Models;

namespace BlazorClientSideRealWorld.Services
{
    public class UserService
    {
        private readonly IJwtService jwtService;
        private readonly IApiService api;
        private readonly StateService state;
        
        public UserService(IJwtService _jwtService, IApiService _api, StateService _state)
        {
            jwtService = _jwtService;
            api = _api;
            state = _state;
        }

        public async Task PopulateAsync()
        {
            string token = await jwtService.GetTokenAsync();

            if (!string.IsNullOrEmpty(token)) {
                api.SetToken(token);
                var response = await api.GetAsync<UserResponse>("/user");
                state.UpdateUser(response?.Value?.User ?? new UserModel());
            }
            else
            {
                await PurgeAuth();
            }
        }

        private async void SetAuth(UserModel user)
        {
            if (user != null)
            {
                await jwtService.SaveTokenAsync(user.Token);
                state.UpdateUser(user);
            }
            else
            {
                await PurgeAuth();
            }

        }

        private async Task PurgeAuth()
        {
            UserModel newUser = new UserModel();
            await jwtService.DestroyTokenAsync();

            if (state?.User != newUser)
                state.UpdateUser(newUser);
        }

        public async Task SignOutAsync()
        {
            await PurgeAuth();
        }

        public async Task<ApiResponse<UserResponse>> AttemptAuth(string authType, UserCredentials credentials)
        {
            var response = await api.PostAsync<UserResponse>($"/users{authType}", new
            {
                user = credentials
            });

            SetAuth(response?.Value?.User);
            return response;
        }

        public async Task<ApiResponse<UserResponse>> UpdateAsync(UserModel user)
        {
            var response = await api.PutAsync<UserResponse>("/user", new
            {
                user = user
            });

            if (response?.Value != null)
                state.UpdateUser(response.Value.User);

            return response;
        }
    }

    public class UserResponse
    {
        public ErrorsModel Errors { get; set; }
        public UserModel User { get; set; }
    }

    public static class AuthenticationType
    {
        public const string Login = "/login";
        public const string Register = "";
    }
}
