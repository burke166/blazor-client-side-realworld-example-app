using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorClientSideRealWorld.Services
{
    public class TagsService
    {
        private IApiService api;

        public TagsService(IApiService _api)
        {
            api = _api;
        }

        public async Task<IEnumerable<string>> QueryAsync(IDictionary<string, string> Params = null)
        {
            var response = await api.GetAsync<TagResponse>($"/tags/", Params);
            return response?.Value?.Tags;
        }

        public async Task<IEnumerable<string>> GetAllAsync()
        {
            return await QueryAsync();
        }
    }

    internal class TagResponse
    {
        public string[] Tags { get; set; }
    }
}
