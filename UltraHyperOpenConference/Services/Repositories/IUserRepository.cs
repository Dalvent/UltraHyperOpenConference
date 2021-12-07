using System.Collections.Generic;
using System.Threading.Tasks;
using UltraHyperOpenConference.Model;

namespace UltraHyperOpenConference.Services.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetAsync(string name);
        public Task<User> GetAsync(string name, string password);
        public Task<List<User>> GetNonApprovedUsers();
    }
}