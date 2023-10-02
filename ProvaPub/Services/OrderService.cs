using Microsoft.AspNetCore.Mvc;
using ProvaPub.Models.DTOs;
using ProvaPub.Models.Entities;
using ProvaPub.Repository.Interfaces;

namespace ProvaPub.Services;

public class OrderService
{
    private readonly CustomerService _customerService;

    public OrderService(CustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task<ActionResult<OrderDTO>> PayOrder(IPaymentMethod selectedPaymentMethod, decimal paymentValue, int customerId)
    {
        bool paymentSuccessful = await selectedPaymentMethod.ProcessPaymentAsync(paymentValue, customerId);

        if (!paymentSuccessful) return null;

        Order order = new Order()
        {
            Value = paymentValue,
            CustomerId = customerId,
            Customer = _customerService.GetItem(customerId)
        };

        return await Task.FromResult(new OrderDTO(order.Id, order.Value, order.OrderDate, order.Customer));
    }
}
