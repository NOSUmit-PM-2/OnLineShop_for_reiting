using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace OnlineShop.DB
{
    public class ProductsDBRepository : IProductDBsRepository
    {
        private readonly DatabaseContext dbContext;

        public ProductsDBRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<ProductDB> GetAll(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return dbContext.ProductDBs.ToList();
            }

            var result = new List<ProductDB>();
            foreach (var product in dbContext.ProductDBs)
            {
                if (product.Description.Contains(searchText))
                {
                    result.Add(product);
                }
            }
            return result;
        }
        public ProductDB TryGetById(Guid id)
        {
            return dbContext.ProductDBs.FirstOrDefault(x => x.Id == id);
        }

        public void Add(ProductDB product)
        {
            dbContext.ProductDBs.Add(product);
            dbContext.SaveChanges();
        }

        public void Updata(ProductDB product)
        {
            var existingProduct = dbContext.ProductDBs.FirstOrDefault(x => x.Id == product.Id);
            if (existingProduct == null)
            {
                return;
            }
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Cost = product.Cost;
            existingProduct.PathPicture = product.PathPicture;
            dbContext.SaveChanges();
        }
    }
}
