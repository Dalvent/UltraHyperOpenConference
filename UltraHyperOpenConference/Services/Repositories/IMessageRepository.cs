﻿using System.Collections.Generic;
using System.Threading.Tasks;
using UltraHyperOpenConference.Model;
using UltraHyperOpenConference.ViewModels;

namespace UltraHyperOpenConference.Services.Repositories
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<List<MessageWithUserName>> GetByThemeAsync(int themeId);
        Task<List<MessageWithUserName>> GetAllByUserAsync(int userId);
        Task<List<MessageWithUserName>> FindInTextAsync(string value);
    }
}