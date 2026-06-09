using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;

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

        public IActionResult Add(Guid id)
        {
            var product = productsRepository.TryGetById(id);
            favRepository.Add(product);
            return RedirectToAction("Catalog", "Product");
        }
    }
}
