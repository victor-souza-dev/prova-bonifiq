using Microsoft.AspNetCore.Mvc;
using ProvaPub.Models.DTOs;
using ProvaPub.Repository.Interfaces;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Controllers;

[ApiController]
[Route("[controller]")]
public class Parte1Controller : ControllerBase
{
    private readonly IRandomService _randomService;
    private readonly INSerialRepository _NSerialRepository;

    public Parte1Controller(IRandomService randomService, INSerialRepository nSerialRepository)
    {
        _randomService = randomService;
        _NSerialRepository = nSerialRepository;
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

    [HttpGet("serial")]
    public async Task<IActionResult> GetSerial(int modelo)
    {
        try
        {
            return Ok(_NSerialRepository.GetSerial(modelo));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("serial")]
    public async Task<IActionResult> Create([FromForm] NSerialDTO nSerial)
    {
        try
        {
            return Ok(_NSerialRepository.CreateSerial(nSerial));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
