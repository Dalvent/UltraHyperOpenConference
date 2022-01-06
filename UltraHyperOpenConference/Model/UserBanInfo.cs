using System.Collections.Generic;
using UltraHyperOpenConference.Model;

namespace UltraHyperOpenConference.Services.Repositories
{
    public class UserBanInfo
    {
        public UserBanInfo(User user, List<BanUser> bans)
        {
            User = user;
            Bans = bans;
        }

        public User User { get; set; }
        public List<BanUser> Bans { get; set; } 
    }
}