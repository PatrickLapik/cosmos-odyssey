using CosmosOdyssey.Dtos;
using CosmosOdyssey.Services;
using Microsoft.AspNetCore.Mvc;

namespace CosmosOdyssey.Controllers;

[ApiController]
[Route("routes")]
public class RouteController : ControllerBase
{
    private readonly ICompanyRouteService _companyRouteService;

    public RouteController(ICompanyRouteService companyRouteService)
    {
        _companyRouteService = companyRouteService;
    }

    [HttpPost]
    public async Task<IActionResult> Get([FromBody] RouteRequest request)
    {
        var routes = _companyRouteService.GetAllRoutes(request);

        return Ok(routes);
    }
}