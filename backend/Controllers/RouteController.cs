using CosmosOdyssey.Data;
using CosmosOdyssey.Dtos;
using CosmosOdyssey.Models;
using CosmosOdyssey.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Pagination.EntityFrameworkCore.Extensions;
using Route = CosmosOdyssey.Models.Route;

namespace CosmosOdyssey.Controllers;

[ApiController]
[Route("[controller]")]
public class RouteController : ControllerBase
{
    private readonly ICompanyRouteService _companyRouteService;
    private readonly IValidator<RouteRequest> _routeRequestValidator;

    public RouteController(ICompanyRouteService companyRouteService, IValidator<RouteRequest> routeRequestValidator)
    {
        _companyRouteService = companyRouteService;
        _routeRequestValidator = routeRequestValidator;
    }
    
    [HttpPost]
    public async Task<IActionResult> Get([FromBody] RouteRequest request)
    {
        var routes = _companyRouteService.GetAllRoutes(request);
        
        return Ok(routes);
    }
}
