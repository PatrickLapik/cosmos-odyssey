using CosmosOdyssey.Data;
using CosmosOdyssey.Models;
using CosmosOdyssey.Services;
using Microsoft.AspNetCore.Mvc;

namespace CosmosOdyssey.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ExternalPriceListService _externalPriceListService;

    public TestController(AppDbContext context, ExternalPriceListService externalPriceListService)
    {
        _context = context;
        _externalPriceListService = externalPriceListService; 
    }

    [HttpGet("test")]
    public async void Test()
    {
        var company = new Company
        {
            Name = "SpaceX"
        };
        
        _context.Add(company);
        await _context.SaveChangesAsync();
    }

    [HttpGet("prices")]
    public async Task<ExternalPriceList> Prices()
    {
        return await _externalPriceListService.GetPriceList();
    }
}
