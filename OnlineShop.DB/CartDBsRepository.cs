using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DB.Models;

namespace OnlineShop.DB
{
    public class CartDBsRepository : ICartDBsRepository
    {
        readonly DatabaseContext databaseContext;
        private readonly UserManager<User> _userManager;

        public CartDBsRepository(DatabaseContext databaseContext, UserManager<User> userManager)
        {
            this.databaseContext = databaseContext;
            _userManager = userManager;
        }

        public void Add(ProductDB product, int userId)
        {
            var currentCart = TryGetByUserId(userId);

            if (currentCart == null)
            {
                var newCart = new CartDB();
                newCart.Id = Guid.NewGuid();
                newCart.UserId = userId;
                newCart.CartItems = new List<CartItemDB>();
                newCart.CartItems.Add(AddItem(product));
                databaseContext.CartDBs.Add(newCart);
            }
            else
            {
                var currentCartItem = currentCart.CartItems.FirstOrDefault(x => x.Product.Id == product.Id);
                if (currentCartItem == null)
                {
                    currentCart.CartItems.Add(AddItem(product));
                }
                else
                {
                    currentCartItem.Amount += 1;
                }
            }
            databaseContext.SaveChangesAsync();

        }


        public void DecreaseCountProduct(Guid productId, int userId)
        {
            var currentCart = TryGetByUserId(userId);
            var currentCartItem = currentCart.CartItems.FirstOrDefault(x => x.Product.Id == productId);
            if (currentCartItem != null)
            {
                currentCartItem.Amount -= 1;
                if (currentCartItem.Amount == 0)
                {
                    currentCart.CartItems.Remove(currentCartItem);
                }
            }
            databaseContext.SaveChangesAsync();
        }

        public void IncreaseCountProduct(Guid productId, int userId)
        {
            var currentCart = TryGetByUserId(userId);
            var currentCartItem = currentCart.CartItems.FirstOrDefault(x => x.Product.Id == productId);
            if (currentCartItem != null)
            {
                currentCartItem.Amount += 1;
            }
            databaseContext.SaveChangesAsync();
        }

        public CartDB TryGetByUserId(int id)
        {

            return databaseContext.CartDBs.
                Include(cart => cart.CartItems)
                .ThenInclude(item => item.Product)
                .FirstOrDefault(cart => cart.UserId == id);
            // return databaseContext.CartDBs.FirstOrDefault(cart => cart.UserId == id);
        }

        CartItemDB AddItem(ProductDB product)
        {
            CartItemDB item = new CartItemDB();
            item.Id = Guid.NewGuid();
            item.Product = product;
            item.Amount = 1;
            return item;
        }

        public void AddFavorite(Guid productId, string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            if (user != null && !user.FavoriteProductIds.Contains(productId))
            {
                var favorites = user.FavoriteProductIds;
                favorites.Add(productId);
                user.FavoriteProductIds = favorites;
                _userManager.UpdateAsync(user).Wait();
            }
        }

        public void RemoveFavorite(Guid productId, string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            if (user != null && user.FavoriteProductIds.Contains(productId))
            {
                var favorites = user.FavoriteProductIds;
                favorites.Remove(productId);
                user.FavoriteProductIds = favorites;
                _userManager.UpdateAsync(user).Wait();
            }
        }

        public List<Guid> GetFavoriteProducts(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            return user?.FavoriteProductIds ?? new List<Guid>();
        }

        public bool IsProductFavorite(Guid productId, string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            return user?.FavoriteProductIds.Contains(productId) ?? false;
        }
    }
}