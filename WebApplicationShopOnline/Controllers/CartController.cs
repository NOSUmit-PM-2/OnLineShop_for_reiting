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
    public class CartController : Controller
    {
        readonly IProductDBsRepository productsRepository;
        readonly ICartDBsRepository cartsRepository;
        readonly IOrderDBsRepository ordersRepository;

        public CartController(IProductDBsRepository prodRepo, ICartDBsRepository cartsRepository, IOrderDBsRepository ordersRepository)
        {
            this.productsRepository = prodRepo;
            this.cartsRepository = cartsRepository;
            this.ordersRepository = ordersRepository;
        }

        public IActionResult Index(int id)
            
        {
            var cartDb = cartsRepository.TryGetByUserId(1);
            var cart = cartDb != null ? Mapping.ToCart(cartDb) : null;
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
        public IActionResult SaveCart(Cart cart, string address)
        {
            ordersRepository.Add(Mapping.ToCartDB(cart), address);
            Console.WriteLine(address);
            return RedirectToAction("Index");
        }
    }
}