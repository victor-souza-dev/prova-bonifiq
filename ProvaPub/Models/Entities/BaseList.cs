namespace ProvaPub.Models.Entities;

public class BaseList<T>
{
    public List<T> Items { get; set; }
    public int TotalCount { get; set; }
    public bool HasNext { get; set; }
}

public class CustomerList : BaseList<Customer>
{
}

public class ProductList : BaseList<Product>
{
}
