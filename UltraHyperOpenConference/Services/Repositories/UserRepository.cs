using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UltraHyperOpenConference.Model;

namespace UltraHyperOpenConference.Services.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(WwwConferenceContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetAsync(string name)
        {
            return await DbSet.FirstOrDefaultAsync(user => user.Name == name);
        }

        public async Task<User> GetAsync(string name, string password)
        {
            return await DbSet.FirstOrDefaultAsync(user => user.Name == name && user.Password == password);
        }

        public async Task<List<User>> GetNonApprovedUsers()
        {
            return await DbSet.Where(item => !item.IsActive).ToListAsync();
        }

        public async Task<List<UserBanInfo>> GetUserBans()
        {
            return await DbSet.Where(item => item.BanUserCapabilityUsers.Any(item => !item.IsArchived))
                .Select(item => new UserBanInfo(item, item.BanUserCapabilityUsers.Where(item => !item.IsArchived).ToList()))
                .ToListAsync();
        }

        public override Task<User> GetByIdAsync(int id)
        {
            return DbSet.FirstOrDefaultAsync(item => item.Id == id);
        }
    }
}