using Microsoft.AspNetCore.Mvc;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Controllers;

[ApiController]
[Route("[controller]")]
public class Parte1Controller : ControllerBase
{
    private readonly IRandomService _randomService;

    public Parte1Controller(IRandomService randomService)
    {
        _randomService = randomService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            return Ok(_randomService.GetRandom().Result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
