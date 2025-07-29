using CosmosOdyssey.Data;
using CosmosOdyssey.Models;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdyssey.Services;

public class CompanyRouteService : BaseService, ICompanyRouteService
{
    public CompanyRouteService(AppDbContext context) : base(context)
    {
    }

    public async Task Save(CompanyRoute route)
    {
        var existing = await Context.Destinations.AnyAsync(d => d.Id == route.Id);
        if (existing) return;
        
        await Context.CompanyRoutes.AddAsync(route);
        await Context.SaveChangesAsync();
    }
}