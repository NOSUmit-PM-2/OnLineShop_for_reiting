using OnlineShop.DB.Models;
using System.Collections.Generic;

namespace OnlineShop.DB
{
    public interface IProductDBsRepository
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}