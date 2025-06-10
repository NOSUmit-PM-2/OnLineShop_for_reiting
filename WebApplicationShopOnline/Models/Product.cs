using System.ComponentModel.DataAnnotations;

namespace WebApplicationShopOnline.Models
{
    public class Product
    {
        static int instanceCounter = 0;

        public Guid Id { get; set; }

        [Required(ErrorMessage = "Не указано название продукта")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Название должно быть от 3 до 100 символов")]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Описание не должно превышать 1000 символов")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Не указана цена")]
        [Range(0.01, 1000000, ErrorMessage = "Цена должна быть от 0.01 до 1000000")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Не указан путь к изображению")]
        [Url(ErrorMessage = "Некорректный URL изображения")]
        [StringLength(500, ErrorMessage = "URL изображения не должен превышать 500 символов")]
        public string PathPicture { get; set; }
    }
}
