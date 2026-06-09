namespace OnlineShop.DB
{
    public class FavProductDB
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public ProductDB Product { get; set; }
    }
}
