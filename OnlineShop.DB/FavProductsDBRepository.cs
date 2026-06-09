using Microsoft.EntityFrameworkCore;

namespace OnlineShop.DB
{
    public class FavProductsDBRepository : IFavProductsDBRepository
    {
        readonly DatabaseContext databaseContext;

        public FavProductsDBRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public List<ProductDB> GetAll()
        {
            return databaseContext.FavProductDBs
                .Include(fav => fav.Product)
                .Select(fav => fav.Product)
                .ToList();
        }

        public void Add(ProductDB product)
        {
            var existing = databaseContext.FavProductDBs
                .FirstOrDefault(fav => fav.ProductId == product.Id);

            if (existing == null)
            {
                databaseContext.FavProductDBs.Add(new FavProductDB
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    Product = product
                });
                databaseContext.SaveChanges();
            }
        }

        public void Remove(Guid productId)
        {
            var fav = databaseContext.FavProductDBs
                .FirstOrDefault(fav => fav.ProductId == productId);

            if (fav != null)
            {
                databaseContext.FavProductDBs.Remove(fav);
                databaseContext.SaveChanges();
            }
        }
    }
}
