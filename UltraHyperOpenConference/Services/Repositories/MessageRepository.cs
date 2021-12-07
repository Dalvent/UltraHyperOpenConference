﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UltraHyperOpenConference.Model;
using UltraHyperOpenConference.ViewModels;

namespace UltraHyperOpenConference.Services.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(WwwConferenceContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<MessageWithUserName>> GetByThemeAsync(int themeId)
        {
            return await DbSet
                .Where(message => message.ThemeId == themeId && !message.IsDeleted)
                .Select(message => new MessageWithUserName(message.UserAuthor.Name, message))
                .ToListAsync();
        }

        public Task<List<MessageWithUserName>> GetAllByUserAsync(int userId)
        {
            return DbSet
                .Where(item => item.UserAuthorId == userId && !item.IsDeleted)
                .Select(message => new MessageWithUserName(message.UserAuthor.Name, message))
                .ToListAsync();
        }

        public Task<List<MessageWithUserName>> FindInTextAsync(string value)
        {
            return DbSet
                .Where(item => item.Text.Contains(value) && !item.IsDeleted)
                .Select(message => new MessageWithUserName(message.UserAuthor.Name, message))
                .ToListAsync();
        }

        public override Task<Message> GetByIdAsync(int id)
        {
            return DbSet.FirstOrDefaultAsync(item => item.Id == id);
        }
    }
}