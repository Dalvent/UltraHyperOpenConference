using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UltraHyperOpenConference.Extensions;
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

        public async Task<TimeSpan> GetRemainingBanSeconds(int userId)
        {
            List<BanUser> activeUserBans = await DbSet
                .Where(item => !item.IsArchived &&  item.UserId == userId && item.CreationDate.AddSeconds(item.DurationInSeconds) > DateTime.Now)
                .ToListAsync();
            
            if (activeUserBans == null || !activeUserBans.Any())
                return TimeSpan.Zero;
            
            long remainingBanTicks = activeUserBans.Select(item => (TimeSpan.TicksPerSecond * item.DurationInSeconds) - (DateTime.Now - item.CreationDate).Ticks).Max();

            return new TimeSpan(remainingBanTicks);
        }
        
        public async Task<BanUser> GetLastRemainingBan(int userId)
        {
            List<BanUser> activeUserBans = await DbSet
                .Where(item => !item.IsArchived &&  item.UserId == userId && item.CreationDate.AddSeconds(item.DurationInSeconds) > DateTime.Now)
                .ToListAsync();
            
            if (activeUserBans == null || !activeUserBans.Any())
                return null;

            BanUser maxBanUser = null;

            foreach (BanUser activeUserBan in activeUserBans)
            {
                if(maxBanUser == null || activeUserBan.GetUnbanTime() > maxBanUser.GetUnbanTime())
                {
                    maxBanUser = activeUserBan;
                }
            }

            return maxBanUser;
        }

        public async Task<bool> IsUserBanned(int userId)
        {
            return await DbSet
                .Where(item => !item.IsArchived && item.UserId == userId && item.CreationDate.AddSeconds(item.DurationInSeconds) > DateTime.Now)
                .AnyAsync();
        }
    }
}