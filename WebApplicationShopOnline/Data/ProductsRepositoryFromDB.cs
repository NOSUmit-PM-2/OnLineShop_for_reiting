using OnlineShop.DB;
using WebApplicationShopOnline.Helpers;
using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Data
{
    public class ProductsRepositoryFromDB : IProductsRepository
    {
        private readonly IProductDBsRepository _productDbRepository;

        public ProductsRepositoryFromDB(IProductDBsRepository productDbRepository)
        {
            _productDbRepository = productDbRepository;
        }

        public List<Product> GetAll()
        {
            var productsDb = _productDbRepository.GetAll();
            return Mapping.ToProductsList(productsDb);
        }

        public Product TryGetById(Guid id)
        {
            var productDb = _productDbRepository.TryGetById(id);
            return productDb == null ? null : Mapping.ToProduct(productDb);
        }

        public void Add(Product product)
        {
            throw new NotImplementedException();
        }

        public void Updata(ProductEdit product)
        {
            throw new NotImplementedException();
        }
    }
}