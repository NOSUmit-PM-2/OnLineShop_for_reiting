using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DB.Models;

namespace OnlineShop.DB
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DbSet<ProductDB> ProductDBs { get; set; }
        public DbSet<CartDB> CartDBs { get; set; }
        public DbSet<CartItemDB> CartItemDBs { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
            InitializeProducts();
        }
        private void InitializeProducts()
        {
            if (!ProductDBs.Any())
            {
                ProductDBs.AddRange(
                    new ProductDB
                    {
                        Id = Guid.NewGuid(),
                        Name = "Чебурек",
                        Description = "с сыром",
                        Cost = 120,
                        PathPicture = "/images/Chebureki.jpg"
                    },
                    new ProductDB
                    {
                        Id = Guid.NewGuid(),
                        Name = "Пирожок",
                        Description = "печеный",
                        Cost = 50,
                        PathPicture = "https://cdn.foodpicasso.com/assets/2022/05/30/2d5c997edd5a1c47de33aa05a85dc987---png_1000x_103c0_convert.png"
                    },
                    new ProductDB
                    {
                        Id = Guid.NewGuid(),
                        Name = "Шаурма",
                        Description = "детская",
                        Cost = 320,
                        PathPicture = "https://bistromania.ru/wp-content/uploads/2022/02/%D1%88%D0%B0%D1%83%D1%80%D0%BC%D0%B0-%D0%B8-%D1%88%D0%B0%D1%83%D1%80%D0%BC%D0%B0-%D0%BC%D0%B8%D0%BD%D0%B8.jpg"
                    }
                );
                SaveChanges();
            }
        }

    }
}