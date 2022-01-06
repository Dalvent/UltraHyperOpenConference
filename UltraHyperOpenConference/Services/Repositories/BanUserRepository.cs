using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UltraHyperOpenConference.Model;

namespace UltraHyperOpenConference.Services.Repositories
{
    public class BanUserRepository : Repository<BanUser>, IBanUserRepository
    {
        public BanUserRepository(WwwConferenceContext dbContext) : base(dbContext)
        {
        }

        public override Task<BanUser> GetByIdAsync(int id)
        {
            return DbSet.FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<int> GetRemainingBanTime(int userId)
        {
            List<BanUser> activeUserBans = await DbSet
                .Where(item => item.UserId == userId && item.CreationDate.AddHours(item.Duration) > DateTime.Now)
                .ToListAsync();
            if (activeUserBans == null || !activeUserBans.Any())
                return -1;

            return (int)activeUserBans.Select(item => item.Duration - (DateTime.Now - item.CreationDate).TotalHours).Max();
        }

        public async Task<bool> IsUserBanned(int userId)
        {
            return await DbSet
                .Where(item => item.UserId == userId && item.CreationDate.AddHours(item.Duration) > DateTime.Now)
                .AnyAsync();
        }
    }
}