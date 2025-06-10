using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using WebApplicationShopOnline.Helpers;
using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductDBsRepository productsRepository;
        private readonly ICartDBsRepository cartsRepository;

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

            if (product != null && product.Quantity > 0)
            {
                // Уменьшаем количество товара
                product.Quantity--;
                productsRepository.Update(product);

                // Добавляем в корзину
                cartsRepository.Add(product, 1);
            }

            return RedirectToAction("Index");
        }

        public IActionResult IncreaseCountProduct(Guid productId)
        {
            var product = productsRepository.TryGetById(productId);
            if (product != null && product.Quantity > 0)
            {
                product.Quantity--;
                productsRepository.Update(product);
                cartsRepository.IncreaseCountProduct(productId, 1);
            }
            return RedirectToAction("Index");
        }

        public IActionResult DecreaseCountProduct(Guid productId)
        {
            var cartItem = cartsRepository.TryGetCartItemByProductId(productId);
            if (cartItem != null)
            {
                // Возвращаем товар в количество
                var product = productsRepository.TryGetById(productId);
                product.Quantity += 1;
                productsRepository.Update(product);     

                cartsRepository.DecreaseCountProduct(productId, 1);
            }
            return RedirectToAction("Index");
        }
    }
}