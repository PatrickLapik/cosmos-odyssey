using CosmosOdyssey.Data;
using CosmosOdyssey.Models;
using CosmosOdyssey.Services.Interfaces;

namespace CosmosOdyssey.Services.Implementations;

public class TravePriceService(AppDbContext context) : BaseService(context), ITravelPriceService
{
    public async Task Save(TravelPrice travelPrice)
    {
        await Context.TravelPrices.AddAsync(travelPrice);
        await Context.SaveChangesAsync();
    }
}