﻿using ProvaPub.Helpers;
using ProvaPub.Infra.Db;
using ProvaPub.Models.Entities;

namespace ProvaPub.Services;

public class ProductService : ListService<Product, ProductList>
{
    private readonly TestDbContext _ctx;
    public ProductService(TestDbContext ctx, QueryManipulator queryManipulator)
        : base(queryManipulator)
    {
        _ctx = ctx;
    }

    protected override IQueryable<Product> GetItemsQuery(string formattedQuery)
    {
        return _ctx.Products
            .OrderBy(p => p.Name)
            .Where(p => p.Name.ToLower().Replace(" ", "").Contains(formattedQuery));
    }

    protected override ProductList CreateList(List<Product> items, int totalCount, bool hasNext)
    {
        var productList = new ProductList
        {
            Items = items,
            TotalCount = totalCount,
            HasNext = hasNext
        };

        return productList;
    }

    protected override Product GetItemQuery(int id)
    {
        throw new NotImplementedException();
    }
}
