using CosmosOdyssey.Services;
using CosmosOdyssey.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CosmosOdyssey.Controllers;

[ApiController]
[Route("travel-prices")]
public class TravelPriceController(ITravelPriceService travelPriceService) : ControllerBase
{
    [HttpGet("valid-until")]
    public async Task<IActionResult> GetValidUntil()
    {
        var validUntil = await travelPriceService.PriceListValidUntil();
        
        return Ok(validUntil);
    }
}