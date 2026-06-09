using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using OnlineShop.DB;
using System.Diagnostics;
using System.Xml.Linq;
using System.Linq;
using WebApplicationShopOnline.Data;
using WebApplicationShopOnline.Helpers;
using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Controllers
{
    public class ProductController : Controller
    {
        readonly IProductsRepository productsRepository; 

        public ProductController(IProductsRepository prodRepo)  
        {
            this.productsRepository = prodRepo;
        }

        public IActionResult Index(Guid id)
        {
            Product prod = productsRepository.TryGetById(id); 
            return View(prod);
        }

        public IActionResult Catalog()
        {
            List<Product> products = productsRepository.GetAll();  
            return View(products); 
        }

        public IActionResult SortByName()
        {
            List<Product> products = productsRepository.GetAll();  

            var sortedProducts = products.OrderBy(p => p.Name).ToList();

            return View("Catalog", sortedProducts); 
        }
    }
}