using Microsoft.AspNetCore.Mvc;
using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Controllers
{
    public class ComparisonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CompareProducts(Product product)
        {
            return View(product);
        }
    }
}

