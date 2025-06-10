using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DB.Models;

namespace OnlineShop.DB
{
    public class DatabaseContext:IdentityDbContext<User>
    {
        public DbSet<ProductDB> ProductDBs { get; set; }
        public DbSet<CartDB> CartDBs { get; set; }
        public DbSet<CartItemDB> CartItemDBs { get; set; }

        public DatabaseContext(DbContextOptions options):base(options)
        {
            Database.Migrate();
        }
    }
}