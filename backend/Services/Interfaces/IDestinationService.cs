using CosmosOdyssey.Dtos.Response;
using CosmosOdyssey.Models;

namespace CosmosOdyssey.Services.Interfaces;

public interface IDestinationService
{
    public Task Save(Destination destination);
    public Task<Destination?> GetByName(string name);
    public Task<List<DestinationResponse>> GetAll();
}