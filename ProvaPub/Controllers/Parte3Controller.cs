using Microsoft.AspNetCore.Mvc;
using ProvaPub.Models.DTOs;
using ProvaPub.Repository.Interfaces;
using ProvaPub.Services;

namespace ProvaPub.Controllers;

[ApiController]
	[Route("[controller]")]
	public class Parte3Controller :  ControllerBase
	{
    private readonly OrderService _orderService;
    private readonly IEnumerable<IPaymentMethod> _paymentMethods;

    public Parte3Controller(OrderService orderService, IEnumerable<IPaymentMethod> paymentMethods)
    {
        _orderService = orderService;
        _paymentMethods = paymentMethods;
    }
    
    [HttpGet("orders")]
		public async Task<ActionResult<OrderDTO>> PlaceOrder(string paymentMethod, decimal paymentValue, int customerId)
		{
        var selectedPaymentMethod = _paymentMethods.FirstOrDefault(p => p.SupportsPaymentMethod(paymentMethod));

        if (selectedPaymentMethod == null)
        {
            return BadRequest("Método de pagamento não suportado.");
        }

        var order = await _orderService.PayOrder(selectedPaymentMethod, paymentValue, customerId);

        if (order == null)
        {
            return BadRequest("O pagamento falhou.");
        }

        return order;
    }
}
