namespace ProvaPub.Models.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public decimal Value { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public Customer Customer { get; set; }

    public Order()
    {
        Id = Guid.NewGuid();
        OrderDate = DateTime.Now;
    }
}
