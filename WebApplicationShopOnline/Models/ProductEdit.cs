using System.ComponentModel.DataAnnotations;

namespace WebApplicationShopOnline.Models
{
    public class ProductEdit
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Не указано название продукта")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Название продукта должно быть от 2 до 30 символов")]
        public string Name { get; set; }
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Описание продукта должно быть от 5 до 200 символов")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Не указана цена продукта")]
        [Range(1, 10000000, ErrorMessage = "Цена продукта должна быть от 1 до 10 000 000")]
        public decimal Cost { get; set; }

        [StringLength(200, MinimumLength = 5, ErrorMessage = "Путь к изображению должен быть от 5 до 200 символов")]
        public string PathPicture { get; set; }
    }
}
