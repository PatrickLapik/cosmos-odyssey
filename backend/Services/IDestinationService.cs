using CosmosOdyssey.Models;

namespace CosmosOdyssey.Services;

public interface IDestinationService
{
    public Task Save(Destination destination);
    public Task<Destination?> GetByName(string name);
    public Task<List<Destination>> GetAll();
}