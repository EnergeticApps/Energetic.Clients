using System.Threading.Tasks;

namespace Energetic.Clients.Navigation
{
    public interface INavigationManager
    {
        Task<bool> IsLoggedInAsync();
        string Location { get; }
        Task NavigateToPageAsync(string url);
        Task NavigateToPageAsync(string url, string parameterKey, string parameterValue);
        Task NavigateToLogInAsync();
        Task NavigateToLogOutAsync();
        Task NavigateToRegisterAsync();
        Task NavigateToManageAccountAsync();
        Task NavigateExternalAsync(string url);
    }
}