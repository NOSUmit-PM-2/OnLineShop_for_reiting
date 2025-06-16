using System.Linq;
using OnlineShop.DB;

namespace WebApplicationShopOnline.Data
{
    public class ProductsDBRepository : IProductDBsRepository
    {
        private readonly DatabaseContext _dbContext;

        public ProductsDBRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ProductDB> GetAll()
        {
            return _dbContext.ProductDBs.ToList();
        }

        public ProductDB TryGetById(Guid id)
        {
            return _dbContext.ProductDBs.FirstOrDefault(x => x.Id == id);
        }

        public void Add(ProductDB product)
        {
            _dbContext.ProductDBs.Add(product);
            _dbContext.SaveChanges();
        }

        public void Update(ProductDB product)
        {
            var existingProduct = _dbContext.ProductDBs.FirstOrDefault(x => x.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Cost = product.Cost;
                existingProduct.PathPicture = product.PathPicture;
                existingProduct.Quantity = product.Quantity;
                _dbContext.SaveChanges();
            }
        }

        public void DecreaseQuantity(Guid productId, int amount = 1)
        {
            var product = _dbContext.ProductDBs.FirstOrDefault(x => x.Id == productId);
            if (product != null)
            {
                product.Quantity -= amount;
                _dbContext.SaveChanges();
            }
        }

        public void IncreaseQuantity(Guid productId, int amount = 1)
        {
            var product = _dbContext.ProductDBs.FirstOrDefault(x => x.Id == productId);
            if (product != null)
            {
                product.Quantity += amount;
                _dbContext.SaveChanges();
            }
        }

        public int GetAvailableQuantity(Guid productId)
        {
            var product = _dbContext.ProductDBs.FirstOrDefault(x => x.Id == productId);
            return product?.Quantity ?? 0;
        }

        public void AddRange(List<ProductDB> products)
        {
            _dbContext.ProductDBs.AddRange(products);
            _dbContext.SaveChanges();
        }
    }
}