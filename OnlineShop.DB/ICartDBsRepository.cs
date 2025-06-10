
namespace OnlineShop.DB
{
    public interface ICartDBsRepository
    {
        CartDB TryGetByUserId(string userId);
        void Add(ProductDB product, string userId);
        void IncreaseCountProduct(Guid productId, string userId);
        void DecreaseCountProduct(Guid productId, string userId);
    }
}