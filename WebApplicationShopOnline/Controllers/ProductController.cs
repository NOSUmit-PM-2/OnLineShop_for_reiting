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

        public IActionResult Catalog(decimal? maxPrice)
        {
            List<ProductDB> products = productsRepository.GetAll();
            List<Product> productViewModels = Mapping.ToProductsList(products);

            if (maxPrice.HasValue && maxPrice.Value > 0)
            {
                productViewModels = productViewModels.Where(p => p.Cost <= maxPrice.Value).ToList();
            }

            ViewBag.MaxPrice = maxPrice;

            return View(productViewModels);
        }
    }
}