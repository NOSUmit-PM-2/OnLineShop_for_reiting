using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using WebApplicationShopOnline.Helpers;
using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductDBsRepository _productsRepository;

        public ProductController(IProductDBsRepository prodRepo)
        {
            _productsRepository = prodRepo;
        }

        public IActionResult Index(Guid id)
        {
            var productDB = _productsRepository.TryGetById(id);
            if (productDB == null)
                return NotFound();

            return View(Mapping.ToProduct(productDB));
        }

        public IActionResult Catalog()
        {
            var products = _productsRepository.GetAll();
            return View(Mapping.ToProductsList(products));
        }
    }
}