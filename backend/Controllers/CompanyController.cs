using CosmosOdyssey.Services;
using CosmosOdyssey.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CosmosOdyssey.Controllers;

[ApiController]
[Route("companies")]
public class CompanyController(ICompanyService companyService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var companies = await companyService.GetAll();
        return Ok(companies);
    }
}