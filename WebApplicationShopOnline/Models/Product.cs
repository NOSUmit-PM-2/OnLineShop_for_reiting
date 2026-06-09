using System.ComponentModel.DataAnnotations;

namespace WebApplicationShopOnline.Models
{
    public class Product
{
    public static int instanceCounter = 0;

    public Guid Id { get; set; }
    [Display(Name = "Название продукта")]
    [Required(ErrorMessage = "Не указано название продукта")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Название должно быть от 3 до 100 символов")]
    public string Name { get; set; }

    [Display(Name = "Описание продукта")]
    [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
    public string Description { get; set; }

    [Display(Name = "Цена")]
    [Range(0.01, 1000000, ErrorMessage = "Цена должна быть больше нуля")]
    public decimal Cost { get; set; }

    [Display(Name = "URL картинки")]
    public string PathPicture { get; set; }
}
}
