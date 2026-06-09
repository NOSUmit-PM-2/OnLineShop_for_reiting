using System;
using System.Collections.Generic;

namespace OnlineShop.DB
{
    public interface IFavoriteRepository
    {
        void Add(Guid productId, string userId = null);
        void Remove(Guid productId, string userId = null);
        List<ProductDB> GetAll(string userId = null);
        bool IsFavorite(Guid productId, string userId = null);
    }
}