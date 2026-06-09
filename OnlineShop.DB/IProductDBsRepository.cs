
namespace OnlineShop.DB
{
    public interface IProductDBsRepository
    {
        List<ProductDB> GetAll(string searchText);
        ProductDB TryGetById(Guid id);
        void Add(ProductDB product);
        void Updata(ProductDB product);
    }
}