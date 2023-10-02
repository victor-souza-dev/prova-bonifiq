using ProvaPub.Helpers;
namespace ProvaPub.Services;

// Template Method
public abstract class ListService<T, TList>
{
    protected readonly QueryManipulator _queryManipulator;

    protected ListService(QueryManipulator queryManipulator)
    {
        _queryManipulator = queryManipulator;
    }

    // Método que realiza paginação em cima do resultado da GetItemsQuery - GetAll
    public TList GetPaginationList(int page = 1, int pageSize = 10, string query = "")
    {
        ValidateParameters(ref page, ref pageSize);

        var formattedQuery = _queryManipulator.FormatQuery(query);
        var itemsQuery = GetItemsQuery(formattedQuery);
        int totalCount = itemsQuery.Count();
        int startIndex = (page - 1) * pageSize;

        var itemsOnPage = itemsQuery
            .Skip(startIndex)
            .Take(pageSize)
            .ToList();

        bool hasNext = startIndex + pageSize < totalCount;

        return CreateList(itemsOnPage, totalCount, hasNext);
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

