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

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(login.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, false);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User {UserName} logged in successfully", login.UserName);
                        return RedirectToAction("Catalog", "Product");
                    }
                    _logger.LogWarning("Failed login attempt for user {UserName}", login.UserName);
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
        public async Task<IActionResult> Registration(Registration reg)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByNameAsync(reg.UserName);
                if (existingUser != null)
                {
                    ModelState.AddModelError("UserName", "Пользователь с таким именем уже существует");
                    _logger.LogWarning("Registration failed: Username {UserName} already exists", reg.UserName);
                    return View(reg);
                }

                var existingEmail = await _userManager.FindByEmailAsync(reg.Email);
                if (existingEmail != null)
                {
                    ModelState.AddModelError("Email", "Пользователь с таким email уже существует");
                    _logger.LogWarning("Registration failed: Email {Email} already exists", reg.Email);
                    return View(reg);
                }

                var user = new User { Email = reg.Email, UserName = reg.UserName };
                var result = await _userManager.CreateAsync(user, reg.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User {UserName} registered successfully", reg.UserName);
                    await _userManager.AddToRoleAsync(user, OnlineShop.DB.Constants.UserRoleName);
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Catalog", "Product");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        _logger.LogWarning("Registration failed for user {UserName}: {Error}", reg.UserName, error.Description);
                    }
                }
            }
            return View(reg);
        }
        public async Task<IActionResult> Logout()
        {
            var userName = User.Identity?.Name;
            await _signInManager.SignOutAsync();
            if (!string.IsNullOrEmpty(userName))
            {
                _logger.LogInformation("User {UserName} logged out", userName);
            }
            return RedirectToAction("Catalog", "Product");
        }
    }
}