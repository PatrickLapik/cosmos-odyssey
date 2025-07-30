using CosmosOdyssey.Models;

namespace CosmosOdyssey.Services.Interfaces;

public interface ITravelPriceService
{
    public Task Save(TravelPrice travelPrice);
}