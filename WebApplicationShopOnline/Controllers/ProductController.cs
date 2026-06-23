using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        public IActionResult Catalog(bool sortByName = false)
        {
            var productDBs = productsRepository.GetAll();

            if (sortByName)
            {
                productDBs = productDBs.OrderBy(p => p.Name).ToList();
            }

            var products = productDBs.Select(p => Mapping.ToProduct(p)).ToList(); 

            return View(products);
        }
    }
}