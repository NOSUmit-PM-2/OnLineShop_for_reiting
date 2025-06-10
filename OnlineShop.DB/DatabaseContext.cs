using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DB.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            builder.Entity<User>()
                .Property(u => u.FavoriteProductIds)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, options),
                    v => JsonSerializer.Deserialize<List<Guid>>(v, options));
        }
    }
}