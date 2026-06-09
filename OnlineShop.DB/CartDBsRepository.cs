using OnlineShop.DB.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.DB
{
    public class CartDBsRepository : ICartDBsRepository
    {
        private readonly DatabaseContext _databaseContext;

        public CartDBsRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<Cart> GetUserCart(string userId)
        {
            return _databaseContext.Carts
                .Where(c => c.UserId == userId)
                .ToList();
        }

        public void AddToCart(string userId, int productId, int quantity)
        {
            var existingItem = _databaseContext.Carts
                .FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var cartItem = new Cart
                {
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity,
                    CreatedAt = DateTime.Now
                };
                _databaseContext.Carts.Add(cartItem);
            }
            _databaseContext.SaveChanges();
        }

        public void RemoveFromCart(int cartId)
        {
            var cartItem = _databaseContext.Carts.FirstOrDefault(c => c.Id == cartId);
            if (cartItem != null)
            {
                _databaseContext.Carts.Remove(cartItem);
                _databaseContext.SaveChanges();
            }
        }

        public void ClearCart(string userId)
        {
            var userCart = GetUserCart(userId);
            _databaseContext.Carts.RemoveRange(userCart);
            _databaseContext.SaveChanges();
        }
    }
}