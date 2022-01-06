using System.Collections.Generic;
using System.Threading.Tasks;
using UltraHyperOpenConference.Model;

namespace UltraHyperOpenConference.Services.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetAsync(string name);
        Task<User> GetAsync(string name, string password);
        Task<List<User>> GetNonApprovedUsers();

        Task<List<UserBanInfo>> GetUserBans();
    }
}