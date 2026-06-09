using System.Linq;

namespace OnlineShop.DB
{
    public class ProductInitializer
    {
        public static void Initialize(IProductDBsRepository productsRepository)
        {
            if (productsRepository.GetAll().Any())
            {
                return;
            }

            var products = new List<ProductDB>
            {
                new ProductDB
                {
                    Id = Guid.NewGuid(),
                    Name = "Чебуреки",
                    Description = "Сочные чебуреки с мясом, обжаренные до золотистой корочки.",
                    Cost = 120,
                    PathPicture = "/images/Chebureki.jpg"
                },
                new ProductDB
                {
                    Id = Guid.NewGuid(),
                    Name = "Шаурма",
                    Description = "Большая шаурма с курицей, свежими овощами и фирменным соусом.",
                    Cost = 250,
                    PathPicture = "/images/saurma.jpg"
                }
            };

            foreach (var product in products)
            {
                productsRepository.Add(product);
            }
        }
    }
}
