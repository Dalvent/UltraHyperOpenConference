using System.Collections.Generic;
using UltraHyperOpenConference.Model;

namespace UltraHyperOpenConference.ViewModels
{
    public class ThemeChatViewModel
    {
        public Theme Theme { get; }
        public ThemeMessageTreeLeaf Chats { get; }

        public ThemeChatViewModel(Theme theme, ThemeMessageTreeLeaf chats)
        {
            Theme = theme;
            Chats = chats;
        }
    }
}