using System;
using System.Collections.Generic;
using UltraHyperOpenConference.Model;

namespace UltraHyperOpenConference.ViewModels
{
    public class ThemeMessageTreeLeaf
    {
        public MessageWithUserName MessageWithAuthorName { get; set; } 
        public List<ThemeMessageTreeLeaf> Answers { get; set; }
    }
}