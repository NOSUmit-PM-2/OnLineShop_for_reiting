using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using OnlineShop.DB;
using System.Diagnostics;
using System.Xml.Linq;
using WebApplicationShopOnline.Data;
using WebApplicationShopOnline.Helpers;
using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Controllers
{
    public class AdminController : Controller
    {
        readonly IProductDBsRepository productsRepository;
        ILogger<AdminController> _logger;

        public AdminController(IProductDBsRepository prodRepo, ILogger<AdminController> logger)
        {
            this.productsRepository = prodRepo;
            _logger = logger;
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
                _logger.LogInformation("Продукт успешно добавлен");
                return RedirectToAction("Products", "Admin");
            }
            else 
            {
                _logger.LogWarning("Данные не прошли проверку");
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
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Данные не прошли проверку");
                return View(product);
            }

            productsRepository.Updata(Mapping.ToProductDB(product));
            _logger.LogInformation("Продукт был обновлен");
            return RedirectToAction("Products");
        }


    }
}