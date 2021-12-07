using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UltraHyperOpenConference.Model;
using UltraHyperOpenConference.Services;
using UltraHyperOpenConference.Services.Repositories;

namespace UltraHyperOpenConference.Controllers
{
    public class ModeratorController : Controller
    {
        private readonly IModerationService _moderationService;
        private readonly IUserRepository _userRepository;

        public ModeratorController(IModerationService moderationService, IUserRepository userRepository)
        {
            _moderationService = moderationService;
            _userRepository = userRepository;
        }
        
        [Authorize(Roles = Constants.ModerRole)]
        public async Task<IActionResult> ApproveUsers()
        {
            List<User> nonApprovedUsers = await _userRepository.GetNonApprovedUsers();
            return View(nonApprovedUsers);
        }

        
        [Authorize(Roles = Constants.ModerRole)]
        public async Task<IActionResult> ApproveUser(int id)
        {
            await _moderationService.ApproveUserAsync(id);
            return Redirect($"../{nameof(ApproveUsers)}");
        }
    }
}