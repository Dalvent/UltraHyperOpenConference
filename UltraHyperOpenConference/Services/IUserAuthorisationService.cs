using System.Threading.Tasks;
using UltraHyperOpenConference.Model;

namespace UltraHyperOpenConference.Services
{
    public interface IUserAuthorisationService
    {
        Task RegisterAsync(string name, string password);
        Task<User> LoginAsync(string name, string password);
        Task LogoutAsync();
    }
}