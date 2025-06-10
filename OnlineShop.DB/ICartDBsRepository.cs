namespace OnlineShop.DB
{
    public interface ICartDBsRepository
    {
        CartDB TryGetByUserId(int id);
        void Add(ProductDB product, int userId);
        void IncreaseCountProduct(Guid productId, int userId);
        void DecreaseCountProduct(Guid productId, int userId);
        void AddFavorite(Guid productId, string userId);
        void RemoveFavorite(Guid productId, string userId);
        List<Guid> GetFavoriteProducts(string userId);
        bool IsProductFavorite(Guid productId, string userId);
    }
}