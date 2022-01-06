using System;
using System.Collections.Generic;
using UltraHyperOpenConference.Model;

namespace UltraHyperOpenConference.ViewModels
{
    public class ThemeChatViewModel
    {
        public Theme Theme { get; }
        public ThemeMessageTreeLeaf Chats { get; }
        public int CurrentUserId { get; }
        public bool IsUserBanned { get; set; }
        public TimeSpan UserBanTime { get; set; }

        public ThemeChatViewModel(Theme theme, ThemeMessageTreeLeaf chats, int currentUserId)
        {
            Theme = theme;
            Chats = chats;
            CurrentUserId = currentUserId;
        }
    }
}