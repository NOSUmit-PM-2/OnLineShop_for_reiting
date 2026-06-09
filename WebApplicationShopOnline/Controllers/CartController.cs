using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using WebApplicationShopOnline.Helpers;
using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Controllers
{
    public class CartController : Controller
    {
        readonly IProductDBsRepository productsRepository;
        readonly ICartDBsRepository cartsRepository;

        public CartController(IProductDBsRepository prodRepo, ICartDBsRepository cartsRepository)
        {
            this.productsRepository = prodRepo;
            this.cartsRepository = cartsRepository;
        }

        public IActionResult Index()
        {
            Cart cart = Mapping.ToCart(cartsRepository.TryGetByUserId(1));
            return View(cart);
        }

        public IActionResult Add(Guid id)
        {
            ProductDB product = productsRepository.TryGetById(id);
            cartsRepository.Add(product, 1);
            return RedirectToAction("Index");
        }

        public IActionResult IncreaseCountProduct(Guid productId)
        {
            cartsRepository.IncreaseCountProduct(productId, 1);
            return RedirectToAction("Index");
        }

        public IActionResult DecreaseCountProduct(Guid productId)
        {
            cartsRepository.DecreaseCountProduct(productId, 1);
            return RedirectToAction("Index");
        }
    }
}