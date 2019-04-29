using System.Threading.Tasks;

namespace BlazorClientSideRealWorld.Services
{
    public interface IConsoleLogService
    {
        Task<bool> LogAsync(string LogString);
    }
}
