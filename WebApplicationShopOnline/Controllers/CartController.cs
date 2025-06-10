using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using OnlineShop.DB.Models;
using WebApplicationShopOnline.Helpers;
using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        readonly IProductDBsRepository productsRepository;
        readonly ICartDBsRepository cartsRepository;
        readonly UserManager<User> userManager;

        public CartController(IProductDBsRepository prodRepo, ICartDBsRepository cartsRepository, UserManager<User> userManager)
        {
            this.productsRepository = prodRepo;
            this.cartsRepository = cartsRepository;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var userId = userManager.GetUserId(User);
            if (userId == null)
                return Unauthorized();

            Cart cart = Mapping.ToCart(cartsRepository.TryGetByUserId(userId));
            return View(cart);
        }

        public IActionResult Add(Guid id)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null)
                return Unauthorized();

            ProductDB product = productsRepository.TryGetById(id);
            cartsRepository.Add(product, userId);
            return RedirectToAction("Index");
        }

        public IActionResult IncreaseCountProduct(Guid productId)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null)
                return Unauthorized();

            cartsRepository.IncreaseCountProduct(productId, userId);
            return RedirectToAction("Index");
        }

        public IActionResult DecreaseCountProduct(Guid productId)
        {
            var userId = userManager.GetUserId(User);
            if (userId == null)
                return Unauthorized();

            cartsRepository.DecreaseCountProduct(productId, userId);
            return RedirectToAction("Index");
        }
    }
}