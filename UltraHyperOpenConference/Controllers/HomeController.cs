using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using UltraHyperOpenConference.Exceptions;
using UltraHyperOpenConference.Extensions;
using UltraHyperOpenConference.Model;
using UltraHyperOpenConference.Services;
using UltraHyperOpenConference.Services.Repositories;
using UltraHyperOpenConference.ViewModels;

namespace UltraHyperOpenConference.Controllers
{
    public class HomeController : Controller
    {
        public const string ErrorTempDataKey = "Error";

        private readonly ILogger<HomeController> _logger;
        private readonly IUserAuthorisationService _userAuthorisationService;


        public HomeController(ILogger<HomeController> logger,
            IUserAuthorisationService userAuthorisationService)
        {
            _logger = logger;
            _userAuthorisationService = userAuthorisationService;
        }

        public IActionResult Index()
        {
            return LocalRedirect("~/Conference/Themes");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData[ErrorTempDataKey] = ModelState.ErrorsToString();
                return View(nameof(Registration));
            }

            try
            {
                await _userAuthorisationService.RegisterAsync(model.Name, model.Password);
            }
            catch (UserAlreadyExistsException)
            {
                TempData[ErrorTempDataKey] = "User with same name is already exist!";
                return View(nameof(Registration));
            }
            catch (Exception e)
            {
                TempData[ErrorTempDataKey] = "Unknown error, try again tomorrow...";
                _logger.LogError(nameof(Registration), e);
                return View(nameof(Registration));
            }

            return Redirect("~/Home/WaitForApprove");
        }

        [HttpGet(nameof(Login))]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult WaitForApprove()
        {
            return View();
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Validate(string login, string password)
        {
            try
            {
                await _userAuthorisationService.LoginAsync(login, password);
            }
            catch (UserNotFoundException)
            {
                TempData[ErrorTempDataKey] = "Username or password is invalid!";
                return View(nameof(Login));
            }
            catch (UserNonActiveException)
            {
                TempData[ErrorTempDataKey] = "User is not approved by administrator! Please, wait...";
                return View(nameof(Login));
            }
            catch (Exception e)
            {
                TempData[ErrorTempDataKey] = "Unknown error, try again tomorrow...";
                _logger.LogError(nameof(Validate), e);
                return View(nameof(Login));
            }

            return LocalRedirect(@"~/Conference/Themes");
        }

        public async Task<IActionResult> Logout()
        {
            await _userAuthorisationService.LogoutAsync();
            return Redirect("/");
        }
    }
}