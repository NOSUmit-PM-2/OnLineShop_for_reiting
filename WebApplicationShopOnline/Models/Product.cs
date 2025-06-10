using System.ComponentModel.DataAnnotations;

namespace WebApplicationShopOnline.Models
{
    public class Product
    {
        static int instanceCounter = 0;

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Не указано название продукта")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Название должно содержать от 3 до 100 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указано описание продукта")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Описание должно содержать от 10 до 500 символов")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Не указана цена продукта")]
        [Range(0.01, 1000000, ErrorMessage = "Цена должна быть больше 0 и меньше 1000000")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Не указан путь к изображению")]
        [Url(ErrorMessage = "Указан некорректный URL изображения")]
        public string PathPicture { get; set; }
    }
}
