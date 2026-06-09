using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DB.Models;

namespace OnlineShop.DB
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Ноутбук Lenovo",
                    Description = "Мощный ноутбук для работы и игр с 16GB RAM",
                    Price = 59990m,
                    Category = "Электроника"
                },
                new Product
                {
                    Id = 2,
                    Name = "Мышь Logitech",
                    Description = "Беспроводная мышь с подсветкой и 6 кнопками",
                    Price = 2490m,
                    Category = "Комплектующие"
                },
                new Product
                {
                    Id = 3,
                    Name = "Клавиатура механическая",
                    Description = "Механическая клавиатура с подсветкой RGB",
                    Price = 4990m,
                    Category = "Комплектующие"
                }
            );
        }
    }
}