using CosmosOdyssey.Data;
using CosmosOdyssey.Models;
using CosmosOdyssey.Services;
using Microsoft.AspNetCore.Mvc;
using Pagination.EntityFrameworkCore.Extensions;
using Route = CosmosOdyssey.Models.Route;

namespace CosmosOdyssey.Controllers;

[ApiController]
[Route("[controller]")]
public class CompanyRouteController : ControllerBase
{
    private readonly ICompanyRouteService _companyRouteService;

    public CompanyRouteController(ICompanyRouteService companyRouteService)
    {
        _companyRouteService = companyRouteService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetBestRoute([FromQuery] int page)
    {
        var companyRoutes = await _companyRouteService.GetPaginated(page);

        return Ok(companyRoutes);
    }
}
