using WebApplicationShopOnline.Models;

public interface IComparisonRepository
{
    void Add(Guid productId);
    void Remove(Guid productId);
    List<Product> GetAll();
    List<Product> GetLastTwo(); 
    bool IsInComparison(Guid productId);
}