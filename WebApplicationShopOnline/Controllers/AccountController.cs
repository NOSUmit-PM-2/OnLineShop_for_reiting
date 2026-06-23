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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Ошибка валидации формы входа для пользователя: {login.UserName}");
                return View(login);
            }

            var user = _userManager.FindByNameAsync(login.UserName).Result;
            if (user != null)
            {
                var result = _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, false).Result;
                if (result.Succeeded)
                {
                    _logger.LogInformation($"Успешный вход пользователя: {login.UserName} ({login.Email})");
                    return RedirectToAction("Catalog", "Product");
                }
            }

            _logger.LogWarning($"Неудачная попытка входа. Имя пользователя: {login.UserName}, Email: {login.Email}");
            ModelState.AddModelError(string.Empty, "Неверное имя пользователя или пароль");
            return View(login);
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(Registration reg)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Ошибка валидации формы регистрации для пользователя: {reg.UserName}");
                return View(reg);
            }

            if (_userManager.FindByNameAsync(reg.UserName).Result == null)
            {
                var user = new User { Email = reg.Email, UserName = reg.UserName };
                var result = _userManager.CreateAsync(user, reg.Password).Result;
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, OnlineShop.DB.Constants.UserRoleName).Wait();
                    _signInManager.SignInAsync(user, false).Wait();
                    _logger.LogInformation($"Успешная регистрация пользователя: {reg.UserName} ({reg.Email})");
                    return RedirectToAction("Catalog", "Product");
                }
                else
                {
                    _logger.LogWarning($"Не удалось зарегистрировать пользователя: {reg.UserName}. Ошибки: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
            else
            {
                _logger.LogWarning($"Попытка регистрации с уже существующим именем пользователя: {reg.UserName}");
                ModelState.AddModelError(string.Empty, "Имя пользователя уже занято");
            }

            return View(reg);
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().Wait();
            _logger.LogInformation($"Пользователь вышел из системы");
            return RedirectToAction("Catalog", "Product");
        }
    }
}