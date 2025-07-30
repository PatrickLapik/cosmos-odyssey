using CosmosOdyssey.Data;
using CosmosOdyssey.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Route = CosmosOdyssey.Models.Route;

namespace CosmosOdyssey.Services.Implementations;

public class RouteService(AppDbContext context) : BaseService(context), IRouteService
{
    public async Task Save(Route route)
    {
        var existing = await GetByFromAndTo(route.FromDestinationId, route.ToDestinationId);
        if (existing != null) return;

        await Context.Routes.AddAsync(route);
        await Context.SaveChangesAsync();
    }

    public async Task<Route?> GetByFromAndTo(Guid fromId, Guid toId)
    {
        return await Context.Routes.Where(r => r.FromDestinationId == fromId && r.ToDestinationId == toId)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Route>> GetAll()
    {
        return await Context.Routes.ToListAsync();
    }
}