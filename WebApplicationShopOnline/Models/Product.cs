using System.ComponentModel.DataAnnotations;

namespace WebApplicationShopOnline.Models
{
    public class Product
    {
        static int instanceCounter = 0;

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Не указано название продукта")]
        public string Name { get; set; }

        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string PathPicture { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
    }

}
