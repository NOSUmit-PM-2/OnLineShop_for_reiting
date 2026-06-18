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
        public DbSet<FavoriteDB> Favorites { get; set; }

        public DatabaseContext(DbContextOptions options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FavoriteDB>(entity =>
            {
                entity.ToTable("Favorites");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Product)
                      .WithMany()
                      .HasForeignKey(e => e.ProductId);
            });

        }
    }
}