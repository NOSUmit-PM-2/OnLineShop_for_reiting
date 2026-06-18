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

        [Range(0, double.MaxValue, ErrorMessage = "Цена не может быть отрицательной")]
        public decimal Cost { get; set; }
        public string PathPicture { get; set; }
    }
}
