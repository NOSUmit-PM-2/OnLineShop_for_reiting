using Microsoft.AspNetCore.Mvc;
using WebApplicationShopOnline.Data;

namespace WebApplicationShopOnline.Controllers
{
    public class ComparisonController : Controller
    {
        private readonly IComparisonRepository _comparisonRepository;

        public ComparisonController(IComparisonRepository comparisonRepository)
        {
            _comparisonRepository = comparisonRepository;
        }

        public IActionResult Index()
        {
            var lastTwo = _comparisonRepository.GetLastTwo();
            return View(lastTwo);
        }

        public IActionResult Add(Guid id)
        {
            _comparisonRepository.Add(id);
            return RedirectToAction("Catalog", "Product");
        }

        public IActionResult Remove(Guid id)
        {
            _comparisonRepository.Remove(id);
            return RedirectToAction("Index");
        }
    }
}