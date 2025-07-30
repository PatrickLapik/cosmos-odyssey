using CosmosOdyssey.Data;
using CosmosOdyssey.Models;

namespace CosmosOdyssey.Services;

public class TravePriceService : BaseService, ITravelPriceService
{
    public TravePriceService(AppDbContext context) : base(context)
    {
    }

    public async Task Save(TravelPrice travelPrice)
    {
        await Context.TravelPrices.AddAsync(travelPrice);
        await Context.SaveChangesAsync();
    }
}