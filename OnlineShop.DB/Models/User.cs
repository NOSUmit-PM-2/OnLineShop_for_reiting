using Microsoft.AspNetCore.Identity;


namespace OnlineShop.DB.Models
{
    public class User:IdentityUser
    {
        public string AvatarUrl { get; set; } = "/images/default-avatar.png";
    }
}
