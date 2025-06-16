using OnlineShop.DB;

public interface ICartDBsRepository
{
    CartDB TryGetByUserId(int userId);
    void Add(ProductDB product, int amount);
    void IncreaseCountProduct(Guid productId, int amount);
    void DecreaseCountProduct(Guid productId, int amount);
    CartItemDB TryGetCartItemByProductId(Guid productId);
}