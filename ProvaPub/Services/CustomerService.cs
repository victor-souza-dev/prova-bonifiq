using Microsoft.EntityFrameworkCore;
using ProvaPub.Helpers;
using ProvaPub.Infra.Db;
using ProvaPub.Models.Entities;

namespace ProvaPub.Services;

public class CustomerService : ListService<Customer, CustomerList>
{
    private readonly TestDbContext _ctx;

    public CustomerService(TestDbContext ctx, QueryManipulator queryManipulator)
        : base(queryManipulator)
    {
        _ctx = ctx;
    }

    protected override IQueryable<Customer> GetItemsQuery(string formattedQuery)
    {
        return _ctx.Customers
            .OrderBy(p => p.Name)
            .Where(p => p.Name.ToLower().Replace(" ", "").Contains(formattedQuery));
    }

    protected override CustomerList CreateList(List<Customer> items, int totalCount, bool hasNext)
    {
        return new CustomerList
        {
            Items = items,
            TotalCount = totalCount,
            HasNext = hasNext
        };
    }

    public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
    {
        if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));

        if (purchaseValue <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseValue));

        //Business Rule: Non registered Customers cannot purchase
        var customer = await _ctx.Customers.FindAsync(customerId);
        if (customer == null) throw new InvalidOperationException($"Customer Id {customerId} does not exists");

        //Business Rule: A customer can purchase only a single time per month
        var baseDate = DateTime.UtcNow.AddMonths(-1);
        var ordersInThisMonth = await _ctx.Orders.CountAsync(s => s.CustomerId == customerId && s.OrderDate >= baseDate);
        if (ordersInThisMonth > 0)
            return false;

        //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
        var haveBoughtBefore = await _ctx.Customers.CountAsync(s => s.Id == customerId && s.Orders.Any());
        if (haveBoughtBefore == 0 && purchaseValue > 100)
            return false;

        return true;
    }

    protected override Customer GetItemQuery(int id)
    {
        var query = _ctx.Customers
            .FirstOrDefault(p => p.Id == id);

        if (query == null) return null;
        
        return query;
    }
}
