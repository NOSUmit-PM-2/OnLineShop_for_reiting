using WebApplicationShopOnline.Models;

namespace WebApplicationShopOnline.Data
{
    public interface IComparisonRepository
    {
        void Add(Guid productId);
        void Remove(Guid productId);
        List<Product> GetAll();
        bool IsInComparison(Guid productId);
    }
}