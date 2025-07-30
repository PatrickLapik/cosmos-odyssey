using CosmosOdyssey.Services;
using CosmosOdyssey.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CosmosOdyssey.Controllers;

[ApiController]
[Route("destinations")]
public class DestinationController(IDestinationService destinationService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var companies = await destinationService.GetAll();
        return Ok(companies);
    }
}