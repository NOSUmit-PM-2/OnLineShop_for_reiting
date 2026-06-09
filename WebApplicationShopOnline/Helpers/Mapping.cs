using OnlineShop.DB;
using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Helpers
{
    public class Mapping
    {
        public static Product ToProduct(ProductDB product)
        {
            if (product == null) return null;
            return new Product
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                PathPicture = product.PathPicture
            };
        }

        public static ProductDB ToProductDB(Product product)
        {
            if (product == null) return null;
            return new ProductDB
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                PathPicture = product.PathPicture
            };
        }

        public static List<Product> ToProductsList(List<ProductDB> productDBs)
        {
            if (productDBs == null) return new List<Product>();
            List<Product> products = new List<Product>();
            foreach (var product in productDBs)
                products.Add(ToProduct(product));
            return products;
        }

        public static List<ProductDB> ToProductDBsList(List<Product> products)
        {
            if (products == null) return new List<ProductDB>();
            List<ProductDB> productDBs = new List<ProductDB>();
            foreach (var product in products)
                productDBs.Add(ToProductDB(product));
            return productDBs;
        }

        public static CartDB ToCartDB(Cart cart)
        {
            if (cart == null) return null;
            return new CartDB
            {
                Id = cart.Id,
                UserId = cart.UserId,
                CartItems = cart.CartItems?.Select(item => ToCartItemDB(item)).ToList()
            };
        }

        private static CartItemDB ToCartItemDB(CartItem item)
        {
            if (item == null) return null;
            return new CartItemDB
            {
                Id = item.Id,
                Product = ToProductDB(item.Product),
                Amount = item.Amount
            };
        }

        public static Cart ToCart(CartDB cart)
        {
            if (cart == null) return null;
            return new Cart
            {
                Id = cart.Id,
                UserId = cart.UserId,
                CartItems = cart.CartItems?.Select(item => ToCartItem(item)).ToList()
            };
        }

        private static CartItem ToCartItem(CartItemDB item)
        {
            if (item == null) return null;
            return new CartItem
            {
                Id = item.Id,
                Product = ToProduct(item.Product),
                Amount = item.Amount
            };
        }
    }
}