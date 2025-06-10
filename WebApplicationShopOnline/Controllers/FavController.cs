using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using OnlineShop.DB.Models;
using WebApplicationShopOnline.Helpers;
using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Controllers
{
    public class FavController : Controller
    {
        private readonly IFavDBRepository favDBRepository;
        private readonly IProductDBsRepository productDBsRepository;

        public FavController(IFavDBRepository favDBRepository, IProductDBsRepository productDBsRepository)
        {
            this.favDBRepository = favDBRepository;
            this.productDBsRepository = productDBsRepository;
        }
        public IActionResult Index()
        {
            var favItems = favDBRepository.GetAll();
            var favProducts = favItems.Select(favItem => productDBsRepository.TryGetById(favItem.ProductId)).ToList();
            return View(Mapping.ToProductsList(favProducts));
        }
        public IActionResult Add(Guid productId)
        {
            FavItemDB favItemDB = new FavItemDB()
            {
                ProductId = productId,
                Id = Guid.NewGuid()

            };
            favDBRepository.Add(favItemDB);
            return RedirectToAction("Catalog", "Product");
        }
    }
}
