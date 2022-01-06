using System;
using System.Collections.Generic;
using UltraHyperOpenConference.Model;

namespace UltraHyperOpenConference.ViewModels
{
    public class UserDetailViewModel
    {
        public User User { get; set; }
        public List<MessageWithUserName> Messages { get; set; }
        public BanUser LastBan { get; set; }
        public bool IsBanned => LastBan?.DurationInSeconds > 0;
    }
}