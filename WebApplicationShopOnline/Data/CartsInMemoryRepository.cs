using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Data
{
    public class CartsInMemoryRepository : ICartRepository
    {
        List<Cart> Carts = new List<Cart>();

        CartItem AddItem(Product product)
        {
            CartItem item = new CartItem();
            item.Id = Guid.NewGuid();
            item.Product = product;
            item.Amount = 1;
            return item;
        }

        public void Add(Product product, int userId)
        {
            var currentCart = TryGetByUserId(userId);

            if (currentCart == null)
            {
                var newCart = new Cart();
                newCart.Id = Guid.NewGuid();
                newCart.UserId = userId;
                newCart.CartItems = new List<CartItem>();
                newCart.CartItems.Add(AddItem(product));
                Carts.Add(newCart);
            }
            else
            {
                var currentCartItem = currentCart.CartItems.FirstOrDefault(x => x.Product.Id == product.Id);
                if (currentCartItem == null)
                {
                    currentCart.CartItems.Add(AddItem(product));
                }
                else
                {
                    currentCartItem.Amount += 1;
                }
            }
        }

        public Cart TryGetByUserId(int id)
        {
            return Carts.FirstOrDefault(x => x.UserId == id);
        }

        public void IncreaseCountProduct(Guid productId, int userId)
        {
            var currentCart = TryGetByUserId(userId);
            var currentCartItem = currentCart.CartItems.FirstOrDefault(x => x.Product.Id == productId);
            if (currentCartItem != null)
            {
                currentCartItem.Amount += 1;
            }
        }

        public void DecreaseCountProduct(Guid productId, int userId)
        {
            var currentCart = TryGetByUserId(userId);
            var currentCartItem = currentCart.CartItems.FirstOrDefault(x => x.Product.Id == productId);
            if (currentCartItem != null)
            {
                currentCartItem.Amount -= 1;
                if (currentCartItem.Amount == 0)
                {
                    currentCart.CartItems.Remove(currentCartItem);
                }
            }
        }
    }
}