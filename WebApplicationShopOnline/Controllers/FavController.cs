using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using WebApplicationShopOnline.Helpers;

namespace WebApplicationShopOnline.Controllers
{
    public class FavController : Controller
    {
        readonly IFavProductsDBRepository favRepository;
        readonly IProductDBsRepository productsRepository;

        public FavController(IFavProductsDBRepository favRepository, IProductDBsRepository productsRepository)
        {
            this.favRepository = favRepository;
            this.productsRepository = productsRepository;
        }

        public IActionResult Index()
        {
            var products = favRepository.GetAll();
            return View(Mapping.ToFavProductsList(products));
        }

        public IActionResult Add(Guid id)
        {
            var product = productsRepository.TryGetById(id);
            favRepository.Add(product);
            return RedirectToAction("Catalog", "Product");
        }

        public IActionResult Remove(Guid id)
        {
            favRepository.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
