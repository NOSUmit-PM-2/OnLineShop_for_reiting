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


        public IActionResult Catalog(decimal? minPrice, decimal? maxPrice)
        {
            List<ProductDB> products = productsRepository.GetAll();
            List<Product> result = Mapping.ToProductsList(products); 
            if (minPrice.HasValue)
            {
                result = result.Where(p => p.Cost >= minPrice.Value).ToList();
            }

            if (maxPrice.HasValue)
            {
                result = result.Where(p => p.Cost <= maxPrice.Value).ToList();
            }

            return View(result);
        }
    }
}

 