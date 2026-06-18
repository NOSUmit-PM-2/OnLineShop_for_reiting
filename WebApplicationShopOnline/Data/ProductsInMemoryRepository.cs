using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Data
{
    public class ProductsInMemoryRepository : IProductsRepository
    {
        static List<Product> products = new List<Product>();
        //{
        //    new Product("Чебурек", "с сыром", 120, "https://swlife.ru/image/cache/catalog/product/02/55/02550518f7517f25a9a7f5da33644fd6-0x0.webp"),
        //    new Product("Пирожок", "с ливером", 50, "https://slastenabakery.ru/image/cache/catalog/tovar/pirozhok-kuritsa-652x495.jpg"),
        //    new Product("Шаурма", "классическая", 400, "/images/saurma.jpg"),
        //    new Product("Шаурма", "детская", 340, "/images/saurma.jpg")
        //};

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
            existingProduct.Length = product.Length;
            existingProduct.Width = product.Width;
            existingProduct.Height = product.Height;
            existingProduct.Weight = product.Weight;
        }

        

    }
}
