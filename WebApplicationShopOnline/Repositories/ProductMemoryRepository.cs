using OnlineShop.DB;
using WebApplicationShopOnline.Models;
using WebApplicationShopOnline.Helpers;

namespace WebApplicationShopOnline.Repositories
{
    public class ProductMemoryRepository : IProductDBsRepository
    {
        private static List<ProductDB> _products = new List<ProductDB>
        {
            new ProductDB
            {
                Id = Guid.NewGuid(),
                Name = "Чебурек",
                Description = "с сыром",
                Cost = 120,
                PathPicture = "https://thumb.tildacdn.com/tild6339-3863-4763-b866-336338656438/-/resize/536x/-/format/webp/cheb.png"
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
            },
            new ProductDB
            {
                Id = Guid.NewGuid(),
                Name = "Блинчик",
                Description = "Тонкий блинчик с начинкой из рубленого мяса и специй.",
                Cost = 70,
                PathPicture = "https://k-expro.ru/wp-content/uploads/2017/06/%D0%91%D0%BB%D0%B8%D0%BD%D1%87%D0%B8%D0%BA-%D0%B1%D0%B5%D0%B7-%D0%BD%D0%B0%D1%87%D0%B8%D0%BD%D0%BA%D0%B8-45%D0%B3%D1%80..jpg"
            },
            new ProductDB
            {
                Id = Guid.NewGuid(),
                Name = "Пельмени",
                Description = "Традиционные русские пельмени с мясной начинкой и специями.",
                Cost = 200,
                PathPicture = "https://moretorg55.ru/d/1639260134_16-papik-pro-p-pelmeni-klipart-26.jpg"
            },
            new ProductDB
            {
                Id = Guid.NewGuid(),
                Name = "Суп",
                Description = "Куриный суп с нежным мясом курицы, овощами и ароматными специями.",
                Cost = 150,
                PathPicture = "https://s1.eda.ru/StaticContent/Photos/4/220207112839/p_O.jpg"
            },
            new ProductDB
            {
                Id = Guid.NewGuid(),
                Name = "Пицца",
                Description = "Классическая пицца Маргарита с томатным соусом и моцареллой!",
                Cost = 400,
                PathPicture = "https://static.pizzasushiwok.ru/images/menu_new/588-1300.jpg"
            },
            new ProductDB
            {
                Id = Guid.NewGuid(),
                Name = "Салат",
                Description = "Легкий салат Цезарь с листьями салата.",
                Cost = 180,
                PathPicture = "https://img.freepik.com/premium-photo/summer-green-salad-white-bowl-plate-isolated-white-backgorund-healty-salat-from-tomato-spinach-onion-green-leaves-with-oil-dinner-lunch-vegetarian-food-concept-top-view_335758-59.jpg"
            },
            new ProductDB
            {
                Id = Guid.NewGuid(),
                Name = "Мороженое",
                Description = "Шоколадное мороженое с густым сливочным вкусом и кусочками шоколада.",
                Cost = 60,
                PathPicture = "https://img.freepik.com/premium-photo/ice-cream-with-chocolate-topping-frozen-yogurt-with-chocolate-isolated-white-background-full-depth-field_1040193-1770.jpg"
            },
            new ProductDB
            {
                Id = Guid.NewGuid(),
                Name = "Торт",
                Description = "вкусный, вкусный",
                Cost = 123,
                PathPicture = "https://fons.grizly.club/uploads/posts/2025-06/04/17490488600575.jpg"
            }
        };

        public List<ProductDB> GetAll()
        {
            return _products;
        }

        public ProductDB TryGetById(Guid id)
        {
            return _products.FirstOrDefault(x => x.Id == id);
        }

        public void Add(ProductDB product)
        {
            product.Id = Guid.NewGuid();
            _products.Add(product);
        }

        public void Updata(ProductDB product)
        {
            var existingProduct = _products.FirstOrDefault(x => x.Id == product.Id);
            if (existingProduct == null)
            {
                return;
            }
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Cost = product.Cost;
            existingProduct.PathPicture = product.PathPicture;
        }
        //bye-bye
    }
}