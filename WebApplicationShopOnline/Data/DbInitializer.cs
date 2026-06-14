using OnlineShop.DB;
using System;
using System.Linq;

namespace OnlineShopp.DB
{
    public static class DbInitializer
    {
        public static void Initialize(DatabaseContext context)
        {
            // Создаём БД, если её нет
            context.Database.EnsureCreated();

            // Проверяем, есть ли уже данные
            if (context.ProductDBs.Any())
                return;

            // Добавляем продукты
            var products = new ProductDB[]
            {
                new ProductDB
                {
                    Id = Guid.Parse("daa0c1cf-7572-47b5-b565-577142018127"),
                    Name = "Чебурек",
                    Description = "с сыром",
                    PathPicture = "https://thumb.tildacdn.com/tild6339-3863-4763-b866-336338656438/-/resize/536x/-/format/webp/cheb.png",
                    Cost = 100
                },
                new ProductDB
                {
                    Id = Guid.Parse("daa0c1cf-7572-47b5-b565-577142018125"),
                    Name = "Пирожок",
                    Description = "печеный",
                    PathPicture = "https://cdn.foodpicasso.com/assets/2022/05/30/2d5c997edd5a1c47de33aa05a85dc987---png_1000x_103c0_convert.png",
                    Cost = 50                },
                new ProductDB
                {
                    Id = Guid.Parse("daa0c1cf-7572-47b5-b565-577142018126"),
                    Name = "Шаурма",
                    Description = "детская",
                    PathPicture = "https://bistromania.ru/wp-content/uploads/2022/02/%D1%88%D0%B0%D1%83%D1%80%D0%BC%D0%B0-%D0%B8-%D1%88%D0%B0%D1%83%D1%80%D0%BC%D0%B0-%D0%BC%D0%B8%D0%BD%D0%B8.jpg",
                    Cost = 300
                },
                new ProductDB
                {
                    Id = Guid.Parse("daa0c1cf-7572-47b5-b565-577142018123"),
                    Name = "Икра",
                    Description = "имитация",
                    PathPicture = "https://moretorg55.ru/d/763c5388b8a310c97cc46f0bbeab94fb.jpg",
                    Cost = 5300
                }
            };

            context.ProductDBs.AddRange(products);
            context.SaveChanges();
        }
    }
}