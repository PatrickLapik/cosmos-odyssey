using CosmosOdyssey.Models;

namespace CosmosOdyssey.Services;

public interface IRouteService
{
    public Task Save(Models.Route route);
    public Task<Models.Route?> GetByFromAndTo(Guid fromId, Guid toId);
    public Task<List<Models.Route>> GetAll();
}