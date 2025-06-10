using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using OnlineShop.DB.Models;
using WebApplicationShopOnline.Helpers;

namespace WebApplicationShopOnline.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly ICartDBsRepository _cartRepository;
        private readonly IProductDBsRepository _productRepository;
        private readonly UserManager<User> _userManager;

        public FavoriteController(
            ICartDBsRepository cartRepository, 
            IProductDBsRepository productRepository,
            UserManager<User> userManager)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = _userManager.GetUserId(User);
            var user = _userManager.FindByIdAsync(userId).Result;
            var favoriteProductIds = _cartRepository.GetFavoriteProducts(user.Id);
            var favoriteProducts = favoriteProductIds
                .Select(id => Mapping.ToProduct(_productRepository.TryGetById(id)))
                .Where(p => p != null)
                .ToList();

            return View(favoriteProducts);
        }

        [HttpPost]
        public IActionResult AddToFavorites(Guid productId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = _userManager.GetUserId(User);
            _cartRepository.AddFavorite(productId, userId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromFavorites(Guid productId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = _userManager.GetUserId(User);
            _cartRepository.RemoveFavorite(productId, userId);
            return RedirectToAction("Index");
        }
    }
} 