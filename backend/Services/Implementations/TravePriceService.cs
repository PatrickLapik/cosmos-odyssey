using CosmosOdyssey.Data;
using CosmosOdyssey.Dtos.Response;
using CosmosOdyssey.Models;
using CosmosOdyssey.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdyssey.Services.Implementations;

public class TravePriceService(AppDbContext context) : BaseService(context), ITravelPriceService
{
    public async Task Save(TravelPrice travelPrice)
    {
        await Context.TravelPrices.AddAsync(travelPrice);
        await Context.SaveChangesAsync();
    }

    public async Task<TravelPriceResponse> PriceListValidUntil()
    {
        var validUntil = await Context.TravelPrices
            .OrderByDescending(tr => tr.ValidUntil)
            .Select(tr => tr.ValidUntil)
            .FirstOrDefaultAsync();

        return new TravelPriceResponse
        {
            ValidUntil = validUntil,
        };
    }
}