using Microsoft.Extensions.Caching.Memory;
using ProvaPub.Helpers;
namespace ProvaPub.Services;

// Template Method
public abstract class PaymentService<T, TList>
{
    private readonly QueryManipulator _queryManipulator;
    private readonly IMemoryCache _memoryCache;

    public PaymentService()
    {
        _queryManipulator = new QueryManipulator();
        _memoryCache = new MemoryCache(new MemoryCacheOptions());
    }

    // Método que realiza paginação em cima do resultado da GetItemsQuery - GetAll
    public TList GetPaginationList(int page = 1, int pageSize = 10, string query = "")
    {
        var formattedQuery = _queryManipulator.FormatQuery(query);
        string ME_KEY = $"{page}_{pageSize}_{formattedQuery}";

        if (_memoryCache.TryGetValue(ME_KEY, out TList me))
        {
            return me;
        }

        var memoryCacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
            SlidingExpiration = TimeSpan.FromSeconds(1200)
        };

        ValidateParameters(ref page, ref pageSize);

        var itemsQuery = GetItemsQuery(formattedQuery);
        int totalCount = itemsQuery.Count();
        int startIndex = (page - 1) * pageSize;

        var itemsOnPage = itemsQuery
            .Skip(startIndex)
            .Take(pageSize)
            .ToList();

        bool hasNext = startIndex + pageSize < totalCount;

        TList payload = CreateList(itemsOnPage, totalCount, hasNext);

        _memoryCache.Set(ME_KEY, payload, memoryCacheEntryOptions);

        return payload;
    }

    // Retorna um elemento específico com base no id
    public T GetItem(int id)
    {
        T itemQuery = GetItemQuery(id);
        return itemQuery;
    }

    // Método para fazer a busca dos dados no banco
    protected abstract IQueryable<T> GetItemsQuery(string formattedQuery);

    protected abstract T GetItemQuery(int id);

    // Método para formatar o retorno dos dados
    protected abstract TList CreateList(List<T> items, int totalCount, bool hasNext);

    private void ValidateParameters(ref int page, ref int pageSize)
    {
        if (page <= 0)
            page = 1;

        if (pageSize <= 0)
            pageSize = 10;
    }
}

