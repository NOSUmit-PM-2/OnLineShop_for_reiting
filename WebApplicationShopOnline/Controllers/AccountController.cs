using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using OnlineShop.DB.Models;
using System.Diagnostics;
using WebApplicationShopOnline.Models;
using OnlineShop.DB;

namespace WebApplicationShopOnline.Controllers
{
    public class AccountController : Controller
    {
        //private readonly IUserManager usersManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            SignInManager<User> signInManager,
            UserManager<User> _userManager,
            ILogger<AccountController> logger)
        {
            _signInManager = signInManager;
            this._userManager = _userManager;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            var user = _userManager.FindByNameAsync(login.UserName).Result;
            if (user != null)
            {
                var result = _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, false).Result;
                if (result.Succeeded)
                {
                    _logger.LogInformation("User {UserName} logged in successfully.", login.UserName);
                    return RedirectToAction("Catalog", "Product");
                }
            }
            _logger.LogWarning("Failed login attempt for user {UserName}.", login.UserName);
            return View(login);
        }

   
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(Registration reg)
        {
            if (_userManager.FindByNameAsync(reg.UserName).Result == null)
            {
                var user = new User { Email = reg.Email, UserName = reg.UserName };
                var result = _userManager.CreateAsync(user, reg.Password).Result;
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, OnlineShop.DB.Constants.UserRoleName).Wait();
                    _signInManager.SignInAsync(user, false).Wait();
                    _logger.LogInformation("User {UserName} registered successfully.", reg.UserName);
                    return RedirectToAction("Catalog", "Product");
                }
            }
            _logger.LogWarning("Registration attempt failed for user {UserName}.", reg.UserName);
            return View();
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().Wait(); 
            _logger.LogInformation("User logged out successfully.");
            return RedirectToAction("Catalog", "Product");
        }

    }
}