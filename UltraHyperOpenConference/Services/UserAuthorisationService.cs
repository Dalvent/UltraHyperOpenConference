using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using UltraHyperOpenConference.Exceptions;
using UltraHyperOpenConference.Model;
using UltraHyperOpenConference.Services.Repositories;

namespace UltraHyperOpenConference.Services
{
    public class UserAuthorisationService : IUserAuthorisationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;

        public UserAuthorisationService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }
        
        public async Task RegisterAsync(string name, string password)
        {
            var user = await _userRepository.GetAsync(name);

            if (user != null)
                throw new UserAlreadyExistsException();
            
            await _userRepository.InsertAsync(new User() { Name = name, Password = password });
        }

        public async Task<User> LoginAsync(string name, string password)
        {
            var user = await _userRepository.GetAsync(name, password);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if(user.IsActive == false)
            {
                throw new UserNonActiveException($"Non active user with id: {user.Id}");
            }

            await LoginWithCookieAuthentication(user);
            
            return user;
        }

        public async Task LogoutAsync()
        {
            await _httpContextAccessor.HttpContext!.SignOutAsync();
        }

        private async Task LoginWithCookieAuthentication(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.Name));
            if(user.IsModer)
                claims.Add(new Claim(ClaimTypes.Role,Constants.ModerRole));
            
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await _httpContextAccessor.HttpContext!.SignInAsync(claimsPrincipal);
        }
    }
}