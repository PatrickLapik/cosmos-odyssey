using CosmosOdyssey.Models;
using CosmosOdyssey.Dtos.Response;

namespace CosmosOdyssey.Services.Interfaces;

public interface ITravelPriceService
{
    public Task Save(TravelPrice travelPrice);
    public Task<TravelPriceResponse> PriceListValidUntil();
}