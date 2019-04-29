using System.Threading.Tasks;
using BlazorClientSideRealWorld.Models;

namespace BlazorClientSideRealWorld.Services
{
    public class ProfilesService
    {
        private readonly IApiService api;

        public ProfilesService(IApiService _api)
        {
            api = _api;
        }

        public async Task<ApiResponse<ProfileResponse>> GetAsync(string username)
        {
            return await api.GetAsync<ProfileResponse>($"/profiles/{username}");
        }

        public async Task<bool> FollowAsync(string username)
        {
            var response = await api.PostAsync<ProfileResponse>($"/profiles/{username}/follow");
            return response?.HasSuccessStatusCode ?? false;
        }

        public async Task<bool> UnfollowAsync(string username)
        {
            var response = await api.DeleteAsync<ProfileResponse>($"/profiles/{username}/follow");
            return response?.HasSuccessStatusCode ?? false;
        }

    }

    public class ProfileResponse
    {
        public ProfileModel Profile { get; set; }
    }
}
