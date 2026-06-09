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
            if (product.Cost < 0)
            {
                ModelState.AddModelError("Cost", "Цена не может быть отрицательной!");
                return View(product);
            }

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
            if (product.Cost < 0)
            {
                ModelState.AddModelError("Cost", "Цена не может быть отрицательной!");
                return View(product);
            }

            productsRepository.Updata(Mapping.ToProductDB(product));
            return RedirectToAction("Index", "Product", new { product.Id });
        }
    }
}