using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using OnlineShop.DB;
using System.Diagnostics;
using System.Xml.Linq;
using WebApplicationShopOnline.Data;
using WebApplicationShopOnline.Helpers;
using WebApplicationShopOnline.Models;
using Microsoft.AspNetCore.Identity;
using OnlineShop.DB.Models;

namespace WebApplicationShopOnline.Controllers
{
    public class AdminController : Controller
    {
        readonly IProductDBsRepository productsRepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(IProductDBsRepository prodRepo, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.productsRepository = prodRepo;
            _userManager = userManager;
            _roleManager = roleManager;
        }

     
        public IActionResult Products(int id)
        {
            return View(Mapping.ToProductsList(productsRepository.GetAll()));
        }


        [HttpGet]
        public IActionResult AddProduct() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
        
            if (ModelState.IsValid)
            {
                productsRepository.Add(Mapping.ToProductDB(product));
                return RedirectToAction("Products", "Admin");
            }
            else 
            {
                return View(product);
            }
        }

        [HttpGet]
        public IActionResult EditProduct(Guid id)
        {
            var productDB = productsRepository.TryGetById(id);
            return View(Mapping.ToProduct(productDB));
        }

        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            productsRepository.Updata(Mapping.ToProductDB(product));
            return RedirectToAction("Index", "Product", new { product.Id });
        }

        public async Task<IActionResult> Users()
        {
            var users = _userManager.Users.ToList();
            var userRoles = new List<UserRoleViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoles.Add(new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }

            return View(userRoles);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var allRoles = new[] { OnlineShop.DB.Constants.AdminRoleName, OnlineShop.DB.Constants.UserRoleName };
            var userRoles = await _userManager.GetRolesAsync(user);

            // Удаляем все роли
            await _userManager.RemoveFromRolesAsync(user, userRoles);

            // Добавляем только выбранную
            await _userManager.AddToRoleAsync(user, roleName);

            return RedirectToAction(nameof(Users));
        }
    }
}