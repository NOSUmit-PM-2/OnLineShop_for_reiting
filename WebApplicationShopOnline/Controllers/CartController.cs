using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using OnlineShop.DB;
using System.Data;
using System.Diagnostics;
using System.Xml.Linq;
using WebApplicationShopOnline.Data;
using WebApplicationShopOnline.Helpers;
using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Controllers
{
    public class CartController : Controller
    {
        readonly IProductDBsRepository productsRepository;
        readonly ICartDBsRepository cartsRepository;
        private readonly ILogger<CartController> _logger;

        public CartController(IProductDBsRepository prodRepo, ICartDBsRepository cartsRepository, ILogger<CartController> logger)
        {
            this.productsRepository = prodRepo;
            this.cartsRepository = cartsRepository;
            _logger = logger;
        }

        public IActionResult Index(int id)
        {
            Cart cart = Mapping.ToCart(cartsRepository.TryGetByUserId(1));
            _logger.LogInformation("User with id {id} opened the cart page", id);
            return View(cart);
        }

        public IActionResult Add(Guid id)
        {
            ProductDB product = productsRepository.TryGetById(id);
            cartsRepository.Add(product, 1);
            _logger.LogInformation("User with id {id} added product with id {productId} to the cart", 1, id);
            return RedirectToAction("Index");
        }

        public IActionResult IncreaseCountProduct(Guid productId)
        {
            cartsRepository.IncreaseCountProduct(productId, 1);
            _logger.LogInformation("User with id {id} increased count of product with id {productId} in the cart", 1, productId);
            return RedirectToAction("Index");
        }

        public IActionResult DecreaseCountProduct(Guid productId)
        {
            cartsRepository.DecreaseCountProduct(productId, 1);
            _logger.LogInformation("User with id {id} decreased count of product with id {productId} in the cart", 1, productId);
            return RedirectToAction("Index");
        }
    }
}