using CosmosOdyssey.Data;
using CosmosOdyssey.Models;
using CosmosOdyssey.Services;
using Microsoft.AspNetCore.Mvc;
using Pagination.EntityFrameworkCore.Extensions;
using Route = CosmosOdyssey.Models.Route;

namespace CosmosOdyssey.Controllers;

[ApiController]
[Route("[controller]")]
public class RouteController : ControllerBase
{
    private readonly ICompanyRouteService _companyRouteService;

    public RouteController(ICompanyRouteService companyRouteService)
    {
        _companyRouteService = companyRouteService;
    }
    
    [HttpGet("")]
    public async Task<IActionResult> Get([FromQuery] Guid from, [FromQuery] Guid to)
    {
        var routes = _companyRouteService.GetAllRoutes(from, to);
        
        return Ok(routes);
    }
}
