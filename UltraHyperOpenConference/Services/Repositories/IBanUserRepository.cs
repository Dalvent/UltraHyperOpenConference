using System;
using System.Threading.Tasks;
using UltraHyperOpenConference.Model;

namespace UltraHyperOpenConference.Services.Repositories
{
    public interface IBanUserRepository : IRepository<BanUser>
    {
        Task<TimeSpan> GetRemainingBanSeconds(int userId);
        Task<bool> IsUserBanned(int userId);
        Task<BanUser> GetLastRemainingBan(int userId);
    }
}