﻿using System;
using System.Collections.Generic;

#nullable disable

namespace UltraHyperOpenConference.Model
{
    public partial class User
    {
        public User()
        {
            BanUserCapabilityModerators = new HashSet<BanUserCapability>();
            BanUserCapabilityUsers = new HashSet<BanUserCapability>();
            Messages = new HashSet<Message>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsModer { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<BanUserCapability> BanUserCapabilityModerators { get; set; }
        public virtual ICollection<BanUserCapability> BanUserCapabilityUsers { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
