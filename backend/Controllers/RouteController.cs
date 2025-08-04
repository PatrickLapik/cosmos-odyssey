using CosmosOdyssey.Dtos;
using CosmosOdyssey.Dtos.Request;
using CosmosOdyssey.Services;
using CosmosOdyssey.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CosmosOdyssey.Controllers;

[ApiController]
[Route("routes")]
public class RouteController(ICompanyRouteService companyRouteService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Get([FromBody] RouteRequest request)
    {
        var routes = companyRouteService.GetAllRoutes(request);

        return Ok(routes);
    }
}
