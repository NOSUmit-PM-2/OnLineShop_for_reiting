using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DB.Models;

namespace OnlineShop.DB
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly DatabaseContext _databaseContext;

        public FavoriteRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        private string GetUserId(string userId = null)
        {
            return userId ?? "anonymous";
        }

        public void Add(Guid productId, string userId = null)
        {
            var currentUserId = GetUserId(userId);

            if (!IsFavorite(productId, currentUserId))
            {
                var favorite = new FavoriteDB
                {
                    Id = Guid.NewGuid(),
                    ProductId = productId,
                    UserId = currentUserId,
                    AddedAt = DateTime.Now
                };
                _databaseContext.Favorites.Add(favorite);
                _databaseContext.SaveChanges();
            }
        }

        public void Remove(Guid productId, string userId = null)
        {
            var currentUserId = GetUserId(userId);
            var favorite = _databaseContext.Favorites
                .FirstOrDefault(f => f.ProductId == productId && f.UserId == currentUserId);

            if (favorite != null)
            {
                _databaseContext.Favorites.Remove(favorite);
                _databaseContext.SaveChanges();
            }
        }

        public List<ProductDB> GetAll(string userId = null)
        {
            var currentUserId = GetUserId(userId);

            return _databaseContext.Favorites
                .Include(f => f.Product)
                .Where(f => f.UserId == currentUserId)
                .Select(f => f.Product)
                .ToList();
        }

        public bool IsFavorite(Guid productId, string userId = null)
        {
            var currentUserId = GetUserId(userId);

            return _databaseContext.Favorites
                .Any(f => f.ProductId == productId && f.UserId == currentUserId);
        }
    }
}