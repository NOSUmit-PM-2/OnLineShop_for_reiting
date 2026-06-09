using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Data
{
    public interface ICartRepository
    {
        Cart TryGetByUserId(int id);
        void Add(Product product, int userId);
        void IncreaseCountProduct(Guid productId, int userId);
        void DecreaseCountProduct(Guid productId, int v);
    }
}