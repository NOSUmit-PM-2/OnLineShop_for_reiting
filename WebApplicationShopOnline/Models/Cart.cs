using OnlineShop.DB;
using WebApplicationShopOnline.Helpers;

namespace WebApplicationShopOnline.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public decimal Cost
        {
            get { return CartItems.Sum(x => x.Cost); }
        }

        public void AddProduct(ProductDB productDB, int amount)
        {
            var product = Mapping.ToProduct(productDB); 
            var existingItem = CartItems.FirstOrDefault(x => x.Product.Id == product.Id);
            if (existingItem != null)
            {
                existingItem.Amount += amount;
            }
            else
            {
                CartItems.Add(new CartItem
                {
                    Product = product, 
                    Amount = amount
                });
            }
        }
    }
}