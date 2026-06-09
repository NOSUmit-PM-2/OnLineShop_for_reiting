using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Data
{
    public class ProductsInMemoryRepository : IProductsRepository
    {
        static List<Product> products = new List<Product>();

        public List<Product> GetAll()
        {
            return products;
        }

        public Product TryGetById(Guid id)
        {
            return products.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Product product)
        {
            products.Add(product);
        }

        public void Updata(ProductEdit product)
        {
            var existingProduct = products.FirstOrDefault(x => x.Id == product.Id);
            if (existingProduct == null)
            {
                return;
            }
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Cost = product.Cost;
            existingProduct.PathPicture = product.PathPicture;
        }
    }
}