using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UltraHyperOpenConference.Model;
using UltraHyperOpenConference.Services.Repositories;
using UltraHyperOpenConference.ViewModels;

namespace UltraHyperOpenConference.Services
{
    public class ConferenceService : IConferenceService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IMessageRepository _messageRepository;
        private readonly IThemeRepository _themeRepository;

        public ConferenceService(ICurrentUserService currentUserService, IMessageRepository messageRepository, IThemeRepository themeRepository)
        {
            _currentUserService = currentUserService;
            _messageRepository = messageRepository;
            _themeRepository = themeRepository;
        }
        
        public async Task<Theme> StartThemeAsync(string name, string startMessage)
        {
            var theme = await InsertNewTheme(name);
            await InsertNewMessage(theme.Id, startMessage);
            return theme;
        }

        public async Task<Message> AnswerToAsync(int parentMessageId, string answer)
        {
            Theme theme = await _themeRepository.GetThemeOfMessage(parentMessageId);
            return await InsertNewMessage(theme.Id, answer, parentMessageId);
        }

        private async Task<Theme> InsertNewTheme(string name)
        {
            Theme theme = new() { Name = name };
            await _themeRepository.InsertAsync(theme);
            return await _themeRepository.GetThemeFromName(name);
        }

        private async Task<Message> InsertNewMessage(int themeId, string text, int? parentMessageId = null)
        {
            Message message = new()
            {
                CreationDate = DateTime.Now,
                ThemeId = themeId,
                ParentMessageId = parentMessageId,
                UserAuthorId = _currentUserService.GetId(),
                Text = text
            };
            await _messageRepository.InsertAsync(message);
            return message;
        }
    }
}