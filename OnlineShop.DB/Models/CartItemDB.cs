using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DB
{
    public class CartItemDB
    {
        public Guid Id { get; set; }
        public ProductDB Product { get; set; }
        public int Amount { get; set; }
        public CartDB Cart { get; set; }
    }
}
