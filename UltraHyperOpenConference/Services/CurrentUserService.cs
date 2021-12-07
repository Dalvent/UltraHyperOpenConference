using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using UltraHyperOpenConference.Model;
using UltraHyperOpenConference.Services.Repositories;

namespace UltraHyperOpenConference.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal ClaimsUser => _httpContextAccessor!.HttpContext!.User;

        public int GetId()
        {
            return Convert.ToInt32(ClaimsUser.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public async Task<User> GetUserAsync()
        {
            return await _userRepository.GetByIdAsync(GetId());
        }

        public bool IsModer()
        {
            return ClaimsUser.IsInRole(Constants.ModerRole);
        }
    }
}