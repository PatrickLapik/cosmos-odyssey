using CosmosOdyssey.Data;
using CosmosOdyssey.Models;
using Microsoft.AspNetCore.Mvc;

namespace CosmosOdyssey.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly AppDbContext _context;

    public TestController(AppDbContext context)
    {
        _context = context;
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
}
