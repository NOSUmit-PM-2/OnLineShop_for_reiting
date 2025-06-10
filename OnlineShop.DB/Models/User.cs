using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace OnlineShop.DB.Models
{
    public class User : IdentityUser
    {
        private string _favoriteProductIds;
        public List<Guid> FavoriteProductIds 
        { 
            get => string.IsNullOrEmpty(_favoriteProductIds) 
                ? new List<Guid>() 
                : JsonSerializer.Deserialize<List<Guid>>(_favoriteProductIds, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            set => _favoriteProductIds = JsonSerializer.Serialize(value, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
