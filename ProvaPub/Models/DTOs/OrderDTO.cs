using ProvaPub.Models.Entities;

namespace ProvaPub.Models.DTOs;

public class OrderDTO
{
    public Guid Id { get; private set; }
    public decimal Value { get; set; }
    public DateTime OrderDate { get; set; }
    public Customer Customer { get; set; }

    public OrderDTO(Guid id, decimal value, DateTime orderDate, Customer customer)
    {
        Id = id;
        Value = value;
        OrderDate = orderDate;
        Customer = customer;
    }
}
