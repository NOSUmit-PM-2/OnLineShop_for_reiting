using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using WebApplicationShopOnline.Helpers;

namespace WebApplicationShopOnline.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IProductDBsRepository _productsRepository;

        public FavoriteController(IFavoriteRepository favoriteRepository, IProductDBsRepository productsRepository)
        {
            _favoriteRepository = favoriteRepository;
            _productsRepository = productsRepository;
        }

        public IActionResult Index()
        {
            var favoriteProducts = _favoriteRepository.GetAll();
            var products = Mapping.ToProductsList(favoriteProducts);
            return View(products);
        }

        [HttpPost]
        public IActionResult Add(Guid productId)
        {
            _favoriteRepository.Add(productId);
            return RedirectToAction("Catalog", "Product");
        }

        [HttpPost]
        public IActionResult Remove(Guid productId)
        {
            _favoriteRepository.Remove(productId);
            return RedirectToAction("Index");
        }
    }
}