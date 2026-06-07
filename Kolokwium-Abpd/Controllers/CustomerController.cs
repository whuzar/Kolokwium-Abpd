using Kolokwium_Abpd.DTOs;
using Kolokwium_Abpd.Exceptions;
using Kolokwium_Abpd.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium_Abpd.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    
    private readonly IDbService _dbService;

    public CustomerController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [HttpGet("{id:int}/purchases")]
    public async Task<IActionResult> GetOrder(int id)
    {
        try
        {
            var order = await _dbService.GetPurchasesById(id);
            return Ok(order);
        }
        catch (NotFoundException e)
        {
            return NotFound();
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateWithPurchases(CreateCustomerWithPurchasesDto request)
    {
        await _dbService.CreateCustomerWithPurchases(request);
        return Created();
    }
    
}