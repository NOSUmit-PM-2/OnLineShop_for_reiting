using Microsoft.AspNetCore.Mvc;
using WebApplicationShopOnline.Data;
using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;

        public ProductController()
        {
            _productRepository = new ProductRepository();
        }

        public IActionResult Index(Guid id)  
        {
            Product product = _productRepository.TryGetById(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        public IActionResult Catalog(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            List<Product> products = _productRepository.GetProducts(searchString);
            return View(products);
        }
    }
}