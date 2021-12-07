using System;
using System.Collections.Generic;

#nullable disable

namespace UltraHyperOpenConference.Model
{
    public partial class UserCapability
    {
        public UserCapability()
        {
            BanUserCapabilities = new HashSet<BanUserCapability>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BanUserCapability> BanUserCapabilities { get; set; }
    }
}
