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
        public DbSet<FavProductDB> FavProductDBs { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
            if (!Database.EnsureCreated())
            {
                Database.ExecuteSqlRaw(@"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'FavProductDBs')
                        CREATE TABLE [FavProductDBs] (
                            [Id] uniqueidentifier NOT NULL,
                            [ProductId] uniqueidentifier NOT NULL,
                            CONSTRAINT [PK_FavProductDBs] PRIMARY KEY ([Id]),
                            CONSTRAINT [FK_FavProductDBs_ProductDBs_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [ProductDBs]([Id]) ON DELETE CASCADE
                        )");
            }
        }
    }
}