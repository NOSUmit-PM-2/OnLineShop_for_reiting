using System.Text.Json;
using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Data
{
    public class ProductRepository
    {
        private static List<Product> _products = new List<Product>();
        private static readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "products.json");

        static ProductRepository()
        {
            LoadProductsFromJson();
        }

        private static void LoadProductsFromJson()
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
     
                var tempProducts = JsonSerializer.Deserialize<List<TempProduct>>(json) ?? new List<TempProduct>();

                _products = tempProducts.Select(p => new Product
                {
                    Id = Guid.NewGuid(), 
                    Name = p.Name,
                    Description = p.Description,
                    Cost = p.Cost,
                    PathPicture = p.PathPicture
                }).ToList();
            }
        }

        public List<Product> GetProducts(string searchString = null)
        {
            if (string.IsNullOrEmpty(searchString))
                return _products.ToList();

            return _products
                .Where(p => p.Name.ToLower().Contains(searchString.ToLower()))
                .ToList();
        }

        public Product TryGetById(Guid id)
        {
            return _products.FirstOrDefault(product => product.Id == id);
        }

        public List<Product> SearchByName(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
                return _products.ToList();

            return _products
                .Where(p => p.Name.ToLower().Contains(searchString.ToLower()))
                .ToList();
        }

        private class TempProduct
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Cost { get; set; }
            public string PathPicture { get; set; }
        }
    }
}