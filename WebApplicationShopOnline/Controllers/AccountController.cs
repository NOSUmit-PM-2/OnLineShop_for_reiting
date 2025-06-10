using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using OnlineShop.DB;
using OnlineShop.DB.Models;
using System.Diagnostics;
using System.Security.Claims;
using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Controllers
{
    public class AccountController : Controller
    {
        //private readonly IUserManager usersManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> _userManager)
        {
            _signInManager = signInManager;
            this._userManager = _userManager;
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
            if (_userManager.FindByNameAsync(reg.UserName).Result == null)
            {
                var user = new User { Email = reg.Email, UserName = reg.UserName };
                var result = _userManager.CreateAsync(user, reg.Password).Result;
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, OnlineShop.DB.Constants.UserRoleName).Wait();
                    _signInManager.SignInAsync(user, false).Wait();
                    return RedirectToAction("Catalog", "Product");
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().Wait();   
            return RedirectToAction("Catalog", "Product");
        }

        [Authorize]  // Доступ только для авторизованных
        public IActionResult Profile()
        {
            // Получаем данные пользователя (например, из БД или Claims)
            var user = new UserProfileModel
            {
                UserName = User.Identity.Name,
                Email = User.FindFirst(ClaimTypes.Email)?.Value,
                AvatarUrl = null  // Если есть аватар - указать путь
            };

            return View(user);
        }

        [Authorize]
        public IActionResult EditProfile()
        {
            // Логика редактирования профиля
            return View();
        }

    }
}