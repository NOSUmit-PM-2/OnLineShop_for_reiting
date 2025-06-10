using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using OnlineShop.DB;
using System.Diagnostics;
using System.Xml.Linq;
using WebApplicationShopOnline.Data;
using WebApplicationShopOnline.Helpers;
using WebApplicationShopOnline.Models;
using Microsoft.Extensions.Logging;

namespace WebApplicationShopOnline.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductDBsRepository productsRepository;
        private readonly ILogger<AdminController> logger;

        public AdminController(IProductDBsRepository prodRepo, ILogger<AdminController> logger)
        {
            this.productsRepository = prodRepo;
            this.logger = logger;
        }

        public IActionResult Products(int id)
        {
            logger.LogInformation("Admin accessed products list");
            return View(Mapping.ToProductsList(productsRepository.GetAll()));
        }

        [HttpGet]
        public IActionResult AddProduct() 
        {
            logger.LogInformation("Admin accessed add product form");
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                productsRepository.Add(Mapping.ToProductDB(product));
                logger.LogInformation("Admin added new product: {ProductName} with ID: {ProductId}", product.Name, product.Id);
                return RedirectToAction("Products", "Admin");
            }
            else 
            {
                logger.LogWarning("Failed to add product. Validation errors: {ValidationErrors}", 
                    string.Join(", ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)));
                return View(product);
            }
        }

        [HttpGet]
        public IActionResult EditProduct(Guid id)
        {
            var productDB = productsRepository.TryGetById(id);
            logger.LogInformation("Admin accessed edit form for product: {ProductId}", id);
            return View(Mapping.ToProduct(productDB));
        }

        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                productsRepository.Updata(Mapping.ToProductDB(product));
                logger.LogInformation("Admin updated product: {ProductName} with ID: {ProductId}", product.Name, product.Id);
                return RedirectToAction("Index", "Product", new { product.Id });
            }
            else
            {
                logger.LogWarning("Failed to update product {ProductId}. Validation errors: {ValidationErrors}", 
                    product.Id,
                    string.Join(", ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)));
                return View(product);
            }
        }
    }
}