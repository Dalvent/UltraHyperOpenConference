﻿using System;
using System.Collections.Generic;

#nullable disable

namespace UltraHyperOpenConference.Model
{
    public partial class BanUserCapability
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ModeratorId { get; set; }
        public int UserCapability { get; set; }
        public string Reason { get; set; }
        public DateTime CreationDate { get; set; }
        public int Duration { get; set; }

        public virtual User Moderator { get; set; }
        public virtual User User { get; set; }
        public virtual UserCapability UserCapabilityNavigation { get; set; }
    }
}
