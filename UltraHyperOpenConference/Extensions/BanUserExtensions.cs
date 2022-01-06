using System;
using UltraHyperOpenConference.Model;

namespace UltraHyperOpenConference.Extensions
{
    public static class BanUserExtensions
    {
        public static DateTime GetUnbanTime(this BanUser banUser)
        {
            return banUser.CreationDate.AddSeconds(banUser.DurationInSeconds);
        }
    }
}