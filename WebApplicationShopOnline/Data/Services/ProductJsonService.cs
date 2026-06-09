using System.Text.Json;
using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Services
{
    public class ProductJsonService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductJsonService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public List<Product> GetProducts()
        {
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "data", "products.json");

            if (!File.Exists(filePath))
            {
                return new List<Product>();
            }

            var jsonData = File.ReadAllText(filePath);
            var products = JsonSerializer.Deserialize<List<ProductJson>>(jsonData);

            return products.Select(p => new Product
            {
                Id = Guid.NewGuid(),
                Name = p.Name,
                Description = p.Description,
                Cost = p.Cost,
                PathPicture = p.PathPicture
            }).ToList();
        }
    }

    public class ProductJson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string PathPicture { get; set; }
    }
}