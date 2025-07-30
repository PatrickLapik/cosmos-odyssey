using Route = CosmosOdyssey.Models.Route;

namespace CosmosOdyssey.Services;

public interface IRouteService
{
    public Task Save(Route route);
    public Task<Route?> GetByFromAndTo(Guid fromId, Guid toId);
    public Task<List<Route>> GetAll();
}