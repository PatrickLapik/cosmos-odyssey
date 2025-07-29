using CosmosOdyssey.Data;
using CosmosOdyssey.Models;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdyssey.Services;

public class DestinationService : BaseService, IDestinationService
{
    public DestinationService(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task Save(Destination destination)
    {
        var existing = await GetByName(destination.Name);
        if (existing != null) return;        
        
        await Context.Destinations.AddAsync(destination);
        await Context.SaveChangesAsync();
    }

    public async Task<Destination?> GetByName(string name)
    {
        return await Context.Destinations.Where(d => d.Name == name).FirstOrDefaultAsync();
    }

    public async Task<List<Destination>> GetAll()
    {
        return await Context.Destinations.ToListAsync();
    }
}