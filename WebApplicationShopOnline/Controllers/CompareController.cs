using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using WebApplicationShopOnline.Helpers;
using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Controllers
{
    public class CompareController : Controller
    {
        readonly IProductDBsRepository productsRepository;

        static List<Product> comparedProducts = new List<Product>();

        public CompareController(IProductDBsRepository prodRepo)
        {
            this.productsRepository = prodRepo;
        }

        public IActionResult Add(Guid id)
        {
            Product product = Mapping.ToProduct(productsRepository.TryGetById(id));

            if (!comparedProducts.Any(p => p.Id == id))
            {
                comparedProducts.Add(product);

                if (comparedProducts.Count > 2)
                {
                    comparedProducts.RemoveAt(0);
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View(comparedProducts);
        }
    }
}
