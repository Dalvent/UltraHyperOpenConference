using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using UltraHyperOpenConference.Model;
using UltraHyperOpenConference.Services;
using UltraHyperOpenConference.Services.Repositories;
using UltraHyperOpenConference.ViewModels;

namespace UltraHyperOpenConference.Controllers
{
    public class ConferenceController : Controller
    {
        private readonly IConferenceService _conferenceService;
        private readonly IBanUserRepository _banUserRepository;
        private readonly IThemeRepository _themeRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IThemeMessageTreeService _themeMessageTreeService;
        private readonly IModerationService _moderationService;
        private readonly ICurrentUserService _currentUserService;

        public ConferenceController(IThemeRepository themeRepository, IConferenceService conferenceService, IMessageRepository messageRepository, IThemeMessageTreeService themeMessageTreeService, IModerationService moderationService, ICurrentUserService currentUserService, IBanUserRepository banUserRepository)
        {
            _themeRepository = themeRepository;
            _conferenceService = conferenceService;
            _messageRepository = messageRepository;
            _themeMessageTreeService = themeMessageTreeService;
            _moderationService = moderationService;
            _currentUserService = currentUserService;
            _banUserRepository = banUserRepository;
        }

        public async Task<IActionResult> Themes()
        {
            return View(await _themeRepository.GetAllAsync());
        }

        [HttpGet(nameof(CreateTheme))]
        public IActionResult CreateTheme()
        {
            return View();
        }

        [HttpPost(nameof(CreateTheme))]
        public async Task<IActionResult> CreateTheme(string theme, string startMessage)
        {
            Theme themeObj = await _conferenceService.StartThemeAsync(theme, startMessage);
            return Redirect($"~/Conference/ThemeChat?themeId={themeObj.Id}");
        }

        [HttpPost(nameof(AnswerTo))]
        public async Task<IActionResult> AnswerTo(int parentId, string messageText)
        {
            var message = await _conferenceService.AnswerToAsync(parentId, messageText);
            return Redirect($"~/Conference/ThemeChat?themeId={message.ThemeId}");
        }

        public async Task<IActionResult> ThemeChat(int themeId)
        {
            Theme theme = await _themeRepository.GetByIdAsync(themeId);
            ThemeMessageTreeLeaf root = await _themeMessageTreeService.GetTreeAsync(themeId);

            var userBanTime = await _banUserRepository.GetRemainingBanSeconds(_currentUserService.GetId());
            
            return View(new ThemeChatViewModel(theme, root, _currentUserService.GetId())
            {
                IsUserBanned = userBanTime != TimeSpan.Zero,
                UserBanTime = userBanTime
            });
        }

        [Authorize(Roles = Constants.ModerRole)]
        public async Task<IActionResult> RemoveMessage(int messageId)
        {
            var removedMessage = await _moderationService.DeleteMessageAsync(messageId);
            return Redirect($"~/Conference/ThemeChat?themeId={removedMessage.ThemeId}");
        }

        [HttpPost]
        public async Task<IActionResult> EditComment(int messageId, string messageNewText)
        {
            var message = await _messageRepository.GetByIdAsync(messageId);
            message.Text = messageNewText;
            await _messageRepository.UpdateAsync(message);

            return Redirect($"~/Conference/ThemeChat?themeId={message.ThemeId}");
        }

        [Authorize(Roles = Constants.ModerRole)]
        [HttpPost]
        public IActionResult BanUserFromSearch(int userId, int banTimeDays, int banTimeHours, int banTimeMinutes, int banTimeSeconds, string reasonText, string keyword)
        {
            int totalBanSeconds = (int)new TimeSpan(banTimeDays, banTimeHours, banTimeMinutes, banTimeSeconds).TotalSeconds;
            _moderationService.BanUserAsync(userId, totalBanSeconds, reasonText);
            return Redirect($"~/Conference/SearchByKeyword?keyword={System.Net.WebUtility.UrlEncode(keyword)}");
        }
        
        [Authorize(Roles = Constants.ModerRole)]
        [HttpPost]
        public IActionResult BanUser(int userId, int banTimeDays, int banTimeHours, int banTimeMinutes, int banTimeSeconds, string reasonText, int redirectThemeId)
        {
            int totalBanSeconds = (int)new TimeSpan(banTimeDays, banTimeHours, banTimeMinutes, banTimeSeconds).TotalSeconds;
            _moderationService.BanUserAsync(userId, totalBanSeconds, reasonText);
            return Redirect($"~/Conference/ThemeChat?themeId={redirectThemeId}");
        }
        
        [Authorize(Roles = Constants.ModerRole)]
        public async Task<IActionResult> ArchiveTheme(int themeId)
        {
            var theme = await _themeRepository.GetByIdAsync(themeId);
            theme.IsArchived = true;
            await _themeRepository.UpdateAsync(theme);
            
            return Redirect("~/Conference/Themes");
        }

        public async Task<IActionResult> SearchByKeyword(string keyword)
        {
            var messages = await _messageRepository.GetByKeyword(keyword) ?? new List<MessageWithUserName>();
            return View(new SearchMessageByKeyword() { Keyword = keyword, Result = messages});
        }
    }
}