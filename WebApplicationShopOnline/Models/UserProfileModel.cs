namespace WebApplicationShopOnline.Models
{
    public class UserProfileModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        //public string Password { get; set; }
        public string? AvatarUrl { get; set; }  // Путь к аватару или null (если стандартный)
    }
}
