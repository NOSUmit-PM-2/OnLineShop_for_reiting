using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DB
{
    public class ProductDB
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string PathPicture { get; set; }
        public ProductCategory Category { get; set; }

    }

    public enum ProductCategory
    {
        HOME,
        ELECTRIC,
        SPORT
    }
}
