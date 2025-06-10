using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DB
{
    public class CartDB
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<CartItemDB> CartItems { get; set; }
    }
}
