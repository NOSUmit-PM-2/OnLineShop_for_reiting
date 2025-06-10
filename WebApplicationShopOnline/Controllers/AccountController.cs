using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShop.DB.Models;
using WebApplicationShopOnline.Models;
using OnlineShop.DB;

namespace WebApplicationShopOnline.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            ILogger<AccountController> logger)
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
                _logger.LogWarning("Валидация не пройдена при входе: {UserName}", login.UserName);
                return View(login);
            }

            _logger.LogInformation("Попытка входа пользователя: {UserName}", login.UserName);
            var user = _userManager.FindByNameAsync(login.UserName).Result;

            if (user != null)
            {
                var result = _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, false).Result;

                if (result.Succeeded)
                {
                    _logger.LogInformation("Пользователь успешно вошёл: {UserName}", login.UserName);
                    return RedirectToAction("Catalog", "Product");
                }
            }

            _logger.LogWarning("Неудачная попытка входа: {UserName}", login.UserName);
            ModelState.AddModelError("", "Неверное имя пользователя или пароль.");
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
                _logger.LogWarning("Валидация не пройдена при регистрации: {UserName}", reg.UserName);
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
                    _logger.LogInformation("Пользователь зарегистрирован: {UserName}", reg.UserName);
                    return RedirectToAction("Catalog", "Product");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    _logger.LogWarning("Ошибки при регистрации: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь с таким именем уже существует.");
                _logger.LogWarning("Попытка регистрации существующего пользователя: {UserName}", reg.UserName);
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
