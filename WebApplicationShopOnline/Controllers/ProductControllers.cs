using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using WebApplicationShopOnline.Helpers;
using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Controllers
{
    public class ProductController : Controller
    {
        readonly IProductDBsRepository productsRepository;

        public ProductController(IProductDBsRepository prodRepo)
        {
            productsRepository = prodRepo;
        }

        public IActionResult Index(Guid id)
        {
            Product prod = Mapping.ToProduct(productsRepository.TryGetById(id));
            return View(prod);
        }

        public IActionResult Catalog(string searchString)
        {
            List<ProductDB> products = productsRepository.GetAll();
            List<Product> productViewModels = Mapping.ToProductsList(products);

            if (!string.IsNullOrEmpty(searchString))
            {
                productViewModels = productViewModels
                    .Where(p => p.Description != null &&
                                p.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            ViewBag.SearchString = searchString;
            return View(productViewModels);
        }
    }
}