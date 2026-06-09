using OnlineShop.DB.Models;
using System.Collections.Generic;

namespace OnlineShop.DB
{
    public interface ICartDBsRepository
    {
        List<Cart> GetUserCart(string userId);
        void AddToCart(string userId, int productId, int quantity);
        void RemoveFromCart(int cartId);
        void ClearCart(string userId);
    }
}