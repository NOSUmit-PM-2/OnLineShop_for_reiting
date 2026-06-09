
namespace OnlineShop.DB
{
    public interface IProductDBsRepository
    {
        List<ProductDB> GetAll();
        ProductDB TryGetById(Guid id);
        void Add(ProductDB product);
        void Updata(ProductDB product);
        List<ProductDB> TryGetByPrice(int min, int max);
    }
}