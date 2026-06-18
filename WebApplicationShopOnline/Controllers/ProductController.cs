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


        public IActionResult Catalog(string? category) 
        {
            List<ProductDB> products;
            if (string.IsNullOrEmpty(category))
            {
                products = productsRepository.GetAll();
                ViewBag.CurrentCategory = "Все товары";
            } else
            {
                ProductCategory productCategory;
                Enum.TryParse(category, out productCategory);

                products = productsRepository.GetByCategory(productCategory) ?? new List<ProductDB>();
                ViewBag.CurrentCategory = GetCategoryDisplayName(productCategory);
            }

            ViewBag.Categories = Enum.GetValues(typeof(ProductCategory))
                .Cast<ProductCategory>()
                .ToDictionary(c => c.ToString(), c => GetCategoryDisplayName(c));

            return View(Mapping.ToProductsList(products));

        }

        private string GetCategoryDisplayName(ProductCategory category)
        {
            string strCategory = string.Empty;
            switch (category)
            {
                case ProductCategory.HOME: strCategory = "Для дома"; break;
                case ProductCategory.ELECTRIC:  strCategory = "Электроника"; break;
                case ProductCategory.SPORT: strCategory = "Спорт";  break;
            };

            return strCategory;
        }

        public IActionResult SeedData()
        {
            var products = new List<ProductDB>
            {
                new() { Id = Guid.NewGuid(), Name = "Диван", Description = "Удобный диван", PathPicture="/images/sofa.png", Cost = 50000, Category = ProductCategory.HOME },
                new() { Id = Guid.NewGuid(), Name = "Телевизор", Description = "4K телевизор", PathPicture="/images/tv.png", Cost = 60000, Category = ProductCategory.ELECTRIC },
                new() { Id = Guid.NewGuid(), Name = "Футбольный мяч", Description = "Кожаный мяч", PathPicture="/images/soccer_ball.png", Cost = 3000, Category = ProductCategory.SPORT }
            };

            foreach (var product in products)
                productsRepository.Add(product);

            return RedirectToAction("Catalog");
        }


    }
}