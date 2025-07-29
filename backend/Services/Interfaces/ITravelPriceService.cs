using CosmosOdyssey.Models;

namespace CosmosOdyssey.Services;

public interface ITravelPriceService
{
    public Task Save(TravelPrice travelPrice);
}