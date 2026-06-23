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

        public AdminController(IProductDBsRepository prodRepo)
        {
            this.productsRepository = prodRepo;
        }

        public IActionResult Products(int id)
        {
            Logger.LogAction("View", "Products list viewed");
            return View(Mapping.ToProductsList(productsRepository.GetAll()));
        }

        [HttpGet]
        public IActionResult AddProduct() 
        {
            Logger.LogAction("View", "Add product form opened");
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    productsRepository.Add(Mapping.ToProductDB(product));
                    Logger.LogAction("Add", $"Product added: {product.Name} (ID: {product.Id})");
                    return RedirectToAction("Products", "Admin");
                }
                catch (Exception ex)
                {
                    Logger.LogAction("Error", $"Failed to add product: {ex.Message}");
                    ModelState.AddModelError("", "Произошла ошибка при добавлении продукта");
                    return View(product);
                }
            }
            else 
            {
                Logger.LogAction("Validation", $"Product validation failed: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");
                return View(product);
            }
        }

        [HttpGet]
        public IActionResult EditProduct(Guid id)
        {
            var productDB = productsRepository.TryGetById(id);
            if (productDB == null)
            {
                Logger.LogAction("Error", $"Product not found for editing: {id}");
                return NotFound();
            }
            Logger.LogAction("View", $"Edit product form opened for product: {id}");
            return View(Mapping.ToProduct(productDB));
        }

        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    productsRepository.Updata(Mapping.ToProductDB(product));
                    Logger.LogAction("Update", $"Product updated: {product.Name} (ID: {product.Id})");
                    return RedirectToAction("Index", "Product", new { product.Id });
                }
                catch (Exception ex)
                {
                    Logger.LogAction("Error", $"Failed to update product: {ex.Message}");
                    ModelState.AddModelError("", "Произошла ошибка при обновлении продукта");
                    return View(product);
                }
            }
            else
            {
                Logger.LogAction("Validation", $"Product validation failed: {string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))}");
                return View(product);
            }
        }
    }
}