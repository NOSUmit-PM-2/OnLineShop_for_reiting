using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Data
{
    public interface ICartRepository
    {
        Cart TryGetByUserId(string userId);
        void Add(Product product, string userId);
        void IncreaseCountProduct(Guid productId, string userId);
        void DecreaseCountProduct(Guid productId, string userId);
    }
}
