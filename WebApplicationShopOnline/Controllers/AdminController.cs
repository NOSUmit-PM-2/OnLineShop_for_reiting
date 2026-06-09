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
        private readonly ILogger<AdminController> _logger;

        public AdminController(IProductDBsRepository prodRepo, ILogger<AdminController> logger)
        {
            this.productsRepository = prodRepo;
            _logger = logger;
        }

     
        public IActionResult Products(int id)
        {
            _logger.LogInformation("Displaying products for admin view.");
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
                _logger.LogInformation("Product {ProductName} added successfully.", product.Name);
                return RedirectToAction("Products", "Admin");
            }
            else 
            {
                _logger.LogWarning("Failed to add product {ProductName}.", product.Name);
                return View(product);
            }
        }

        [HttpGet]
        public IActionResult EditProduct(Guid id)
        {
            var productDB = productsRepository.TryGetById(id);
            _logger.LogInformation("Editing product with ID {ProductId}.", id);
            return View(Mapping.ToProduct(productDB));
        }

        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            productsRepository.Updata(Mapping.ToProductDB(product));
            _logger.LogInformation("Product {ProductName} updated successfully.", product.Name);
            return RedirectToAction("Index", "Product", new { product.Id });
        }
    }
}