using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BlazorClientSideRealWorld.Models;

namespace BlazorClientSideRealWorld.Services
{
    public class ApiService : IApiService
    {
        const string BaseUrl = "https://conduit.productionready.io/api";

        private HttpClient httpClient;
        private IConsoleLogService console;

        public ApiService(HttpClient _httpClient, IConsoleLogService _console)
        {
            httpClient = _httpClient;
            console = _console;
        }

        public void SetToken(string token)
        {
            if (!string.IsNullOrEmpty(token))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", token);
        }

        public void ClearToken()
        {
            httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<ApiResponse<T>> GetAsync<T>(string Path, IDictionary<string, string> Params = null)
        {
            HttpResponseMessage response = await httpClient.GetAsync(BuildUri(Path, Params));
            
            return await ApiResponse<T>.CreateAsync(response);
        }

        public async Task<ApiResponse<T>> PutAsync<T>(string Path, IDictionary<string, string> Params, object Value = null)
        {
            if (Value == null) Value = string.Empty;
            string valueString = JsonConvert.SerializeObject(Value);
            StringContent content = new StringContent(valueString, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync(BuildUri(Path, Params), content);

            return await ApiResponse<T>.CreateAsync(response);
        }

        public async Task<ApiResponse<T>> PutAsync<T>(string Path, object Value = null)
        {
            return await PutAsync<T>(Path, null, Value);
        }

        public async Task<ApiResponse<T>> PostAsync<T>(string Path, IDictionary<string, string> Params, object Value = null)
        {
            if (Value == null) Value = string.Empty;
            string valueString = JsonConvert.SerializeObject(Value);
            StringContent content = new StringContent(valueString, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(BuildUri(Path, Params), content);
            
            return await ApiResponse<T>.CreateAsync(response);
        }

        public async Task<ApiResponse<T>> PostAsync<T>(string Path, object Value = null)
        {
            return await PostAsync<T>(Path, null, Value);
        }

        public async Task<ApiResponse<T>> DeleteAsync<T>(string Path, IDictionary<string, string> Params = null)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync(BuildUri(Path, Params));

            return await ApiResponse<T>.CreateAsync(response);
        }

        protected string BuildUri(string Path, IDictionary<string, string> Params = null)
        {
            UriBuilder result = new UriBuilder($"{BaseUrl}{Path}");

            if (Params != null && Params.Count > 0)
            {
                foreach(string key in Params.Keys)
                {
                    string queryPart = key + "=" + Params[key];
                    if (result.Query != null && result.Query.Length > 1)
                        result.Query = result.Query.Substring(1) + "&" + queryPart;
                    else
                        result.Query = queryPart;
                }
            }

            return result.Uri.AbsoluteUri;
        }
    }

    public class ApiResponse<T>
    {
        public T Value { get; set; }
        public ErrorsModel Errors { get; set; }
        public System.Net.HttpStatusCode StatusCode { get; set; }
        public bool HasSuccessStatusCode { get; set; }

        private ApiResponse()
        {

        }
        
        private async Task<ApiResponse<T>> InitalizeAsync(HttpResponseMessage response)
        {
            StatusCode = response.StatusCode;
            HasSuccessStatusCode = response.IsSuccessStatusCode;
            Errors = new ErrorsModel();
            Value = default(T);
            var data = await response.Content.ReadAsStringAsync();
            
            if (response.IsSuccessStatusCode)
            {
                Value = JsonConvert.DeserializeObject<T>(data);
            }
            else
            {
                try
                {
                    ErrorResponse errors = JsonConvert.DeserializeObject<ErrorResponse>(data);
                    Errors = errors?.Errors;
                }
                catch
                {
                    Console.WriteLine(data);
                }
            }

            return this;
        }

        public static Task<ApiResponse<T>> CreateAsync(HttpResponseMessage response)
        {
            var result = new ApiResponse<T>();
            return result.InitalizeAsync(response);
        }
    }

    internal class ErrorResponse
    {
        public ErrorsModel Errors { get; set; }
    }

}
