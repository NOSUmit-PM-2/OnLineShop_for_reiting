namespace OnlineShop.DB
{
    public interface IFavProductsDBRepository
    {
        List<ProductDB> GetAll();
        void Add(ProductDB product);
        void Remove(Guid productId);
    }
}
