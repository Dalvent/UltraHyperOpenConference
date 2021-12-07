using System.Threading.Tasks;
using UltraHyperOpenConference.Model;

namespace UltraHyperOpenConference.Services
{
    public interface ICurrentUserService
    {
        bool IsModer();
        int GetId();
        Task<User> GetUserAsync();
    }
}