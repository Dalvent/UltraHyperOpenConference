using System.Threading.Tasks;
using UltraHyperOpenConference.Model;

namespace UltraHyperOpenConference.Services.Repositories
{
    public interface IBanUserRepository : IRepository<BanUser>
    {
        Task<int> GetRemainingBanTime(int userId);
        Task<bool> IsUserBanned(int userId);
    }
}