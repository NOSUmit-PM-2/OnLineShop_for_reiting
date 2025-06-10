using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using OnlineShop.DB;
using OnlineShop.DB.Models;
using System.Diagnostics;
using System.Xml.Linq;
using WebApplicationShopOnline.Data;
using WebApplicationShopOnline.Helpers;
using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        readonly IProductDBsRepository productsRepository;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController(IProductDBsRepository prodRepo, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.productsRepository = prodRepo;
            this.roleManager = roleManager;
            this.userManager = userManager;
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

        public IActionResult Roles()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }
    }
}