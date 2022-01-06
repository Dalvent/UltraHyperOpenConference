using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UltraHyperOpenConference.Model;
using UltraHyperOpenConference.Services;
using UltraHyperOpenConference.Services.Repositories;
using UltraHyperOpenConference.ViewModels;

namespace UltraHyperOpenConference.Controllers
{
    public class MessagesController : Controller
    {
        private readonly IConferenceService _conferenceService;
        private readonly IThemeRepository _themeRepository;
        private readonly IMessageRepository _messageRepository;

        public MessagesController(IConferenceService conferenceService, IThemeRepository themeRepository, IMessageRepository messageRepository)
        {
            _conferenceService = conferenceService;
            _themeRepository = themeRepository;
            _messageRepository = messageRepository;
        }

        public async Task<IActionResult> Index(int messageIndex)
        {
            MessageWithUserName message = await _messageRepository.GetWithNameByIdAsync(messageIndex);
            MessageWithUserName parentMessage = null;

            if (message.Message.ParentMessageId.HasValue && message.Message.ParentMessageId != -1)
            {
                parentMessage = await _messageRepository.GetWithNameByIdAsync(message.Message.ParentMessageId.Value);
            }
            
            MessageDetailViewModel messageDetailViewModel = new MessageDetailViewModel()
            {
                Message = message,
                ParentMessage = parentMessage,
                Answers = await _messageRepository.GetAnswersAsync(message.Message.Id)
            };
            return View(messageDetailViewModel);
        }
    }
}