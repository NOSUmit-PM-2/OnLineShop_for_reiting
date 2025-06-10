using System.ComponentModel.DataAnnotations;

namespace WebApplicationShopOnline.Models
{
    public class Registration
    {
        [Required(ErrorMessage = "Имя пользователя обязательно")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Имя пользователя должно быть от 3 до 50 символов")]
        [RegularExpression(@"^[a-zA-Z0-9._-]+$", ErrorMessage = "Имя пользователя может содержать только буквы, цифры, точки, дефисы и подчеркивания")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен быть от 6 до 100 символов")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,}$",
            ErrorMessage = "Пароль должен содержать минимум одну строчную букву, одну заглавную букву, одну цифру и один специальный символ")]
        public string Password { get; set; }
    }
}