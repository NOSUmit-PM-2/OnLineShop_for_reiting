using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Models;
using System.Diagnostics;
using WebApplicationShopOnline.Models;
using OnlineShop.DB;
using Microsoft.Extensions.Logging;

namespace WebApplicationShopOnline.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, ILogger<AccountController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
                    _logger.LogInformation($"Пользователь {login.UserName} вошел в систему");
                    return RedirectToAction("Catalog", "Product");
                }
            }
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
            if (ModelState.IsValid)
            {
                var user = new User { Email = reg.Email, UserName = reg.UserName };
                var result = _userManager.CreateAsync(user, reg.Password).Result;
                
                if (result.Succeeded)
                {
                    _logger.LogInformation($"Пользователь {reg.UserName} зарегистрирован");
                    _userManager.AddToRoleAsync(user, Constants.UserRoleName).Wait();
                    _signInManager.SignInAsync(user, false).Wait();
                    return RedirectToAction("Catalog", "Product");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        _logger.LogWarning($"Ошибка регистрации: {error.Description}");
                    }
                }
            }
            return View(reg);
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().Wait();
            _logger.LogInformation("Пользователь вышел из системы");
            return RedirectToAction("Catalog", "Product");
        }
    }
}