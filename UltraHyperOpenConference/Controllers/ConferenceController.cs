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
    public class ConferenceController : Controller
    {
        private readonly IConferenceService _conferenceService;
        private readonly IThemeRepository _themeRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IThemeMessageTreeService _themeMessageTreeService;

        public ConferenceController(IThemeRepository themeRepository, IConferenceService conferenceService, IMessageRepository messageRepository, IThemeMessageTreeService themeMessageTreeService)
        {
            _themeRepository = themeRepository;
            _conferenceService = conferenceService;
            _messageRepository = messageRepository;
            _themeMessageTreeService = themeMessageTreeService;
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

        public async Task<IActionResult> ThemeChat(int themeId)
        {
            Theme theme = await _themeRepository.GetByIdAsync(themeId);
            ThemeMessageTreeLeaf root = await _themeMessageTreeService.GetTreeAsync(themeId);
            
            return View(new ThemeChatViewModel(theme, root));
        }
    }
}