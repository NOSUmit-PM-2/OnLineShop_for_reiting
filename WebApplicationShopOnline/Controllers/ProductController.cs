using Microsoft.AspNetCore.Mvc;
using WebApplicationShopOnline.Models;
using WebApplicationShopOnline.Repositories;
using WebApplicationShopOnline.Helpers;

namespace WebApplicationShopOnline.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductMemoryRepository _productRepository;

        public ProductController(ProductMemoryRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Catalog()
        {
            var products = _productRepository.GetAll();
            return View(products);
        }

        public IActionResult AddToCompare(Guid id)
        {
            var product = _productRepository.GetById(id);

            if (product == null)
                return NotFound();

            List<Product> compareList = HttpContext.Session.GetObject<List<Product>>("CompareList") ?? new List<Product>();

            if (!compareList.Any(p => p.Id == id))
            {
                compareList.Insert(0, product);
                compareList = compareList.Take(2).ToList();
                HttpContext.Session.SetObject("CompareList", compareList);
            }

            return RedirectToAction("Catalog");
        }

        public IActionResult Compare()
        {
            List<Product> compareList = HttpContext.Session.GetObject<List<Product>>("CompareList") ?? new List<Product>();
            return View(compareList);
        }
    }
}