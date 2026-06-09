using System.ComponentModel.DataAnnotations;

namespace WebApplicationShopOnline.Models
{
    public class Product
    {
        public Guid Id { get; set; } 

        [Required(ErrorMessage = "Не указано название продукта")]
        public string Name { get; set; }

        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string PathPicture { get; set; }
    }
}