using System.ComponentModel.DataAnnotations;

namespace WebApplicationShopOnline.Models
{
    public class ProductEdit
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Не указано название продукта")]
        public string Name { get; set; }

        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string PathPicture { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public double Weight { get; set; }
    }
}
