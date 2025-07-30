using AutoMapper;
using CosmosOdyssey.Data;
using CosmosOdyssey.Dtos;
using CosmosOdyssey.Models;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdyssey.Services;

public class DestinationService : BaseService, IDestinationService
{
    private readonly IMapper _mapper;

    public DestinationService(AppDbContext dbContext, IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
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

    public async Task<List<DestinationResponse>> GetAll()
    {
        var destinations = await Context.Destinations.ToListAsync();

        return _mapper.Map<List<Destination>, List<DestinationResponse>>(destinations);
    }
}