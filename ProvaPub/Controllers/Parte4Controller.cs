using Microsoft.AspNetCore.Mvc;
using ProvaPub.Services;

namespace ProvaPub.Controllers;

[ApiController]
[Route("[controller]")]
public class Parte4Controller : ControllerBase
{
    CustomerService _customerService;
    public Parte4Controller(CustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("CanPurchase")]
    public async Task<IActionResult> CanPurchase(int customerId, decimal purchaseValue)
    {
        try
        {
            return Ok(await _customerService.CanPurchase(customerId, purchaseValue));
        }
        catch
        {
            return BadRequest(false);
        }
    }
}
