using AutoMapper;
using CosmosOdyssey.Data;
using CosmosOdyssey.Dtos.Response;
using CosmosOdyssey.Models;
using CosmosOdyssey.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdyssey.Services.Implementations;

public class DestinationService(AppDbContext dbContext, IMapper mapper) : BaseService(dbContext), IDestinationService
{
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

    public async Task<List<DestinationResponse>> GetAll()
    {
        var destinations = await Context.Destinations.ToListAsync();

        return mapper.Map<List<Destination>, List<DestinationResponse>>(destinations);
    }
}