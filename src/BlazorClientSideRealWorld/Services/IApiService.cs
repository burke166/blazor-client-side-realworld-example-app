using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorClientSideRealWorld.Services
{
    public interface IApiService
    {
        void SetToken(string token);
        void ClearToken();
        Task<ApiResponse<T>> GetAsync<T>(string Path, IDictionary<string, string> Params = null);
        Task<ApiResponse<T>> PutAsync<T>(string Path, IDictionary<string, string> Params, object Value = null);
        Task<ApiResponse<T>> PutAsync<T>(string Path, object Value = null);
        Task<ApiResponse<T>> PostAsync<T>(string Path, IDictionary<string, string> Params, object Value = null);
        Task<ApiResponse<T>> PostAsync<T>(string Path, object Value = null);
        Task<ApiResponse<T>> DeleteAsync<T>(string Path, IDictionary<string, string> Params = null);
    }
}
