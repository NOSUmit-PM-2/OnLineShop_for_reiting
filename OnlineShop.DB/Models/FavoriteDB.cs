using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DB.Models
{
    public class FavoriteDB
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public ProductDB Product { get; set; }
        public string? UserId { get; set; }
        public DateTime AddedAt { get; set; }
    }
}