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

        public IActionResult Catalog(string searchString)
        {
            List<ProductDB> products = productsRepository.GetAll();
            List<Product> productViewModels = Mapping.ToProductsList(products);

            // Фильтрация по подстроке в описании, если указан поисковый запрос
            if (!string.IsNullOrEmpty(searchString))
            {
                productViewModels = productViewModels
                    .Where(p => p.Description != null &&
                                p.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Передаем значение searchString в представление для отображения в поле поиска
            ViewBag.SearchString = searchString;

            return View(productViewModels);
        }
    }
}