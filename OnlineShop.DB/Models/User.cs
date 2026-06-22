using Microsoft.AspNetCore.Identity;


namespace OnlineShop.DB.Models
{
    public class User:IdentityUser
    {
        public string Namber { get; set; }
        public string Address { get; set; }
    }
}
