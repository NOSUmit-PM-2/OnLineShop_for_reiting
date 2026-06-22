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


        /* public IActionResult Catalog() 
         {
             List<ProductDB>products = productsRepository.GetAll();
             //return View("CatalogSimple", products);
             return View(Mapping.ToProductsList(products));
         }*/
        public IActionResult Catalog(decimal? minPrice, decimal? maxPrice)
        {
            List<ProductDB> products = productsRepository.GetAll();

            if (minPrice.HasValue || maxPrice.HasValue)
            {
                products = products.Where(p =>
                    (!minPrice.HasValue || p.Cost >= minPrice.Value) &&
                    (!maxPrice.HasValue || p.Cost <= maxPrice.Value)
                ).ToList();
            }

            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;

            return View(Mapping.ToProductsList(products));
        }
    }
}