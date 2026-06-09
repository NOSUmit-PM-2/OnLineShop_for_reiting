using System.Text.Json;
using WebApplicationShopOnline.Data;
using WebApplicationShopOnline.Models;

public class ComparisonInMemoryRepository : IComparisonRepository
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IProductsRepository _productsRepository;
    private const string SessionKey = "_ComparisonList";

    public ComparisonInMemoryRepository(IHttpContextAccessor httpContextAccessor, IProductsRepository productsRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _productsRepository = productsRepository;
    }

    private List<Guid> GetListFromSession()
    {
        var session = _httpContextAccessor.HttpContext.Session;
        var json = session.GetString(SessionKey);
        return string.IsNullOrEmpty(json) ? new List<Guid>() : JsonSerializer.Deserialize<List<Guid>>(json);
    }

    private void SaveListToSession(List<Guid> ids)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        session.SetString(SessionKey, JsonSerializer.Serialize(ids));
    }

    public void Add(Guid productId)
    {
        var ids = GetListFromSession();
        ids.Remove(productId);
        ids.Add(productId);
        SaveListToSession(ids);
    }

    public void Remove(Guid productId)
    {
        var ids = GetListFromSession();
        ids.Remove(productId);
        SaveListToSession(ids);
    }

    public List<Product> GetAll()
    {
        var ids = GetListFromSession();
        return ids.Select(id => _productsRepository.TryGetById(id))
                  .Where(p => p != null)
                  .ToList();
    }

    public List<Product> GetLastTwo()
    {
        var all = GetAll();
        return all.Skip(Math.Max(0, all.Count - 2)).ToList();
    }

    public bool IsInComparison(Guid productId)
    {
        var ids = GetListFromSession();
        return ids.Contains(productId);
    }
}