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

        public void SeedProducts()
        {
            if (ProductDBs.Any())
            {
                return;
            }

            var products = new List<ProductDB>
            {
                new() { Id = Guid.NewGuid(), Name = "Чебурек", Description = "с сыром", Cost = 120, PathPicture = "https://thumb.tildacdn.com/tild6339-3863-4763-b866-336338656438/-/resize/536x/-/format/webp/cheb.png" },
                new() { Id = Guid.NewGuid(), Name = "Пирожок", Description = "печеный", Cost = 50, PathPicture = "https://cdn.foodpicasso.com/assets/2022/05/30/2d5c997edd5a1c47de33aa05a85dc987---png_1000x_103c0_convert.png" },
                new() { Id = Guid.NewGuid(), Name = "Шаурма", Description = "детская", Cost = 320, PathPicture = "https://bistromania.ru/wp-content/uploads/2022/02/%D1%88%D0%B0%D1%83%D1%80%D0%BC%D0%B0-%D0%B8-%D1%88%D0%B0%D1%83%D1%80%D0%BC%D0%B0-%D0%BC%D0%B8%D0%BD%D0%B8.jpg" },
                new() { Id = Guid.NewGuid(), Name = "Блинчик", Description = "Тонкий блинчик с начинкой из рубленого мяса и специй.", Cost = 70, PathPicture = "https://k-expro.ru/wp-content/uploads/2017/06/%D0%91%D0%BB%D0%B8%D0%BD%D1%87%D0%B8%D0%BA-%D0%B1%D0%B5%D0%B7-%D0%BD%D0%B0%D1%87%D0%B8%D0%BD%D0%BA%D0%B8-45%D0%B3%D1%80..jpg" },
                new() { Id = Guid.NewGuid(), Name = "Бургер", Description = "Сочный бургер с говяжьей котлетой, плавленым сыром и хрустящим беконом.", Cost = 250, PathPicture = "https://chefrestoran.ru/wp-content/uploads/2018/10/604655519.jpg" },
                new() { Id = Guid.NewGuid(), Name = "Пельмени", Description = "Традиционные русские пельмени с мясной начинкой и специями.", Cost = 200, PathPicture = "https://moretorg55.ru/d/1639260134_16-papik-pro-p-pelmeni-klipart-26.jpg" },
                new() { Id = Guid.NewGuid(), Name = "Суп", Description = "Куриный суп с нежным мясом курицы, овощами и ароматными специями.", Cost = 150, PathPicture = "https://s1.eda.ru/StaticContent/Photos/4/220207112839/p_O.jpg" },
                new() { Id = Guid.NewGuid(), Name = "Пицца", Description = "Классическая пицца Маргарита с томатным соусом и моцареллой.", Cost = 400, PathPicture = "https://static.pizzasushiwok.ru/images/menu_new/588-1300.jpg" },
                new() { Id = Guid.NewGuid(), Name = "Салат", Description = "Легкий салат Цезарь с листьями салата, куриной грудкой и соусом Цезарь.", Cost = 180, PathPicture = "https://img.freepik.com/premium-photo/summer-green-salad-white-bowl-plate-isolated-white-backgorund-healty-salat-from-tomato-spinach-onion-green-leaves-with-oil-dinner-lunch-vegetarian-food-concept-top-view_335758-59.jpg" },
                new() { Id = Guid.NewGuid(), Name = "Мороженое", Description = "Шоколадное мороженое с густым сливочным вкусом и кусочками шоколада.", Cost = 60, PathPicture = "https://img.freepik.com/premium-photo/ice-cream-with-chocolate-topping-frozen-yogurt-with-chocolate-isolated-white-background-full-depth-field_1040193-1770.jpg" }
            };

            ProductDBs.AddRange(products);
            SaveChanges();
        }
    }
}