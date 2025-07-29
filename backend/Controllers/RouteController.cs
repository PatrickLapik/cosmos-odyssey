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
