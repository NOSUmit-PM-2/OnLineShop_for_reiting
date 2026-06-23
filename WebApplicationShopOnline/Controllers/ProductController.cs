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
    public class ProductController : Controller
    {
        readonly IProductDBsRepository productsRepository;

        public ProductController(IProductDBsRepository prodRepo)
        {
            this.productsRepository = prodRepo;
        }

        public IActionResult Index(Guid id)
        {
            Product prod = Mapping.ToProduct(productsRepository.TryGetById(id));
            return View(prod);
        }

        public IActionResult Catalog(string searchQuery) 
        {
            List<ProductDB> products = productsRepository.GetAll();
            
            if (!string.IsNullOrEmpty(searchQuery))
            {
                products = products.Where(p => p.Description.ToLower().Contains(searchQuery.ToLower())).ToList();
            }
            
            ViewBag.SearchQuery = searchQuery;
            return View(Mapping.ToProductsList(products));
        }
    }
}