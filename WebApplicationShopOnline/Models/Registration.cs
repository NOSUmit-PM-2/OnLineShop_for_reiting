using System.ComponentModel.DataAnnotations;

namespace WebApplicationShopOnline.Models
{
    public class Registration
    {
        [Required(ErrorMessage = "Введите имя пользователя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите email")]
        [EmailAddress(ErrorMessage = "Неверный формат email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен содержать минимум 6 символов")]
        public string Password { get; set; }
    }
}
