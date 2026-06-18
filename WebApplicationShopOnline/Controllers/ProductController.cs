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


        public IActionResult Catalog() 
        {
            List<ProductDB>products = productsRepository.GetAll();
            //return View("CatalogSimple", products);
            return View(Mapping.ToProductsList(products));
        }

        public IActionResult GenerateProducts()
        {
            Random random = new Random();
            List<ProductDB> testProducts = new List<ProductDB>
            {
                new ProductDB
                {
                    Id = Guid.NewGuid(),
                    Name = "Пицца с сыром",
                    Description = "Горячий, хрустящий, много сыра",
                    Cost = 150,
                    PathPicture = "https://bistromania.ru/wp-content/uploads/2022/02/%D1%88%D0%B0%D1%83%D1%80%D0%BC%D0%BC%D0%B0-%D0%B8-%D1%88%D0%B0%D1%83%D1%80%D0%BC%D0%BC%D0%B0-%D0%BC%D0%B8%D0%BD%D0%B8.jpg",
                    Length = 30,
                    Width = 35,
                    Height = 3,
                    Weight = 1
                },
                new ProductDB
                {
                    Id = Guid.NewGuid(),
                    Name = "Шаурма Классическая",
                    Description = "Свежие овощи, курица и фирменный соус",
                    Cost = 280,
                    PathPicture = "https://bistromania.ru/wp-content/uploads/2022/02/%D1%88%D0%B0%D1%83%D1%80%D0%BC%D0%BC%D0%B0-%D0%B8-%D1%88%D0%B0%D1%83%D1%80%D0%BC%D0%BC%D0%B0-%D0%BC%D0%B8%D0%BD%D0%B8.jpg",
                    Length = 10,
                    Width = 20,
                    Height = 2,
                    Weight = 0.5
                },
                new ProductDB
                {
                    Id = Guid.NewGuid(),
                    Name = "Пицца Маргарита",
                    Description = "Классика на тонком тесте с моцареллой",
                    Cost = 450,
                    PathPicture = "https://static.pizzasushiwok.ru/images/menu_new/588-1300.jpg",
                    Length = 30,
                    Width = 30,
                    Height = 2,
                    Weight = 0.5
                }
            };

            foreach (var product in testProducts)
            {
                productsRepository.Add(product);
            }

            return RedirectToAction("Catalog");
        }
    }
}