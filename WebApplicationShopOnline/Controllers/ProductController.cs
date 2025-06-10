using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using OnlineShop.DB;
using System.Diagnostics;
using System.Xml.Linq;
using WebApplicationShopOnline.Data;
using WebApplicationShopOnline.Helpers;
using WebApplicationShopOnline.Models;
using System.Linq;

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


        public IActionResult Catalog((string sort = null) 
        {
            List<ProductDB> products = productsRepository.GetAll();
            //return View("CatalogSimple", products);
            
            if (sort == "price")
            {
                products = products.OrderBy(p => p.Cost).ToList();
            }
            return View(Mapping.ToProductsList(products));
        }
    }
}