using System.Threading.Tasks;

namespace BlazorClientSideRealWorld.Services
{
    public interface IJwtService
    {
        Task<string> GetTokenAsync();
        Task<bool> SaveTokenAsync(string Token);
        Task<bool> DestroyTokenAsync();
    }
}
