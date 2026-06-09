using Microsoft.AspNetCore.Http;
using System.Text.Json;
using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Data
{
    public class ComparisonInMemoryRepository : IComparisonRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductsRepository _productsRepository;
        private const string SessionKey = "_Comparison";

        public ComparisonInMemoryRepository(IHttpContextAccessor httpContextAccessor, IProductsRepository productsRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _productsRepository = productsRepository;
        }

        private List<Guid> GetIdsFromSession()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var json = session.GetString(SessionKey);
            return string.IsNullOrEmpty(json) ? new List<Guid>() : JsonSerializer.Deserialize<List<Guid>>(json);
        }

        private void SaveIdsToSession(List<Guid> ids)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.SetString(SessionKey, JsonSerializer.Serialize(ids));
        }

        public void Add(Guid productId)
        {
            var ids = GetIdsFromSession();
            if (!ids.Contains(productId))
                ids.Add(productId);
            SaveIdsToSession(ids);
        }

        public void Remove(Guid productId)
        {
            var ids = GetIdsFromSession();
            ids.Remove(productId);
            SaveIdsToSession(ids);
        }

        public List<Product> GetAll()
        {
            var ids = GetIdsFromSession();
            var products = new List<Product>();
            foreach (var id in ids)
            {
                var product = _productsRepository.TryGetById(id);
                if (product != null)
                    products.Add(product);
            }
            return products;
        }

        public bool IsInComparison(Guid productId)
        {
            var ids = GetIdsFromSession();
            return ids.Contains(productId);
        }

        public List<Product> GetLastTwo()
        {
            throw new NotImplementedException();
        }
    }
}