using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Data
{
    public class ProductRepository
    {
        private static List<Product> products = new List<Product>();

        public List<Product> GetProducts()
        {
            return products;
        }

        public Product TryGetById(Guid id)
        {
            return products.FirstOrDefault(product => product.Id == id);
        }
    }
}