using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UltraHyperOpenConference.Model;
using UltraHyperOpenConference.Services;
using UltraHyperOpenConference.Services.Repositories;
using UltraHyperOpenConference.ViewModels;

namespace UltraHyperOpenConference.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IBanUserRepository _banUserRepository;
        private readonly IModerationService _moderationService;

        public UserController(IUserRepository userRepository, IMessageRepository messageRepository, IBanUserRepository banUserRepository, IModerationService moderationService)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _banUserRepository = banUserRepository;
            _moderationService = moderationService;
        }

        public async Task<IActionResult> Index(int userId)
        {
            User user = await _userRepository.GetByIdAsync(userId);
            List<MessageWithUserName> messages = await _messageRepository.GetAllByUserAsync(userId);
            BanUser lastBan = await _banUserRepository.GetLastRemainingBan(userId);
            
            return View(new UserDetailViewModel()
            {
                User = user,
                Messages = messages,
                LastBan = lastBan
            });
        }
        
        [Authorize(Roles = Constants.ModerRole)]
        [HttpPost]
        public IActionResult BanUser(int userId, int banTimeDays, int banTimeHours, int banTimeMinutes, int banTimeSeconds, string reasonText)
        {
            int totalBanSeconds = (int)new TimeSpan(banTimeDays, banTimeHours, banTimeMinutes, banTimeSeconds).TotalSeconds;
            _moderationService.BanUserAsync(userId, totalBanSeconds, reasonText);
            return Redirect($"Users?themeId={userId}");
        }
    }
}