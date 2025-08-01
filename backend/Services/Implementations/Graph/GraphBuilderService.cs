using CosmosOdyssey.Data;
using CosmosOdyssey.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdyssey.Services.Implementations.Graph;

public class GraphBuilderService(IServiceScopeFactory serviceScopeFactory) : IGraphBuilderService
{
    private readonly Dictionary<Guid, GraphNode> _graph = new();

    public async Task LoadGraph()
    {
        using var scope = serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetService<AppDbContext>();

        _graph.Clear();

        var allValidCompanyRoutes = await context.CompanyRoutes
            .Include(cr => cr.Route)
            .ThenInclude(r => r.FromDestination)
            .Include(cr => cr.Route)
            .ThenInclude(r => r.ToDestination)
            .Include(cr => cr.Company)
            .Include(cr => cr.TravelPrice)
            .Where(cr => cr.TravelPrice.ValidUntil >= DateTime.Now)
            .ToListAsync();

        foreach (var route in allValidCompanyRoutes)
        {
            var fromId = route.Route.FromDestinationId;
            var toId = route.Route.ToDestinationId;

            if (!_graph.TryGetValue(fromId, out var fromNode))
            {
                fromNode = new GraphNode { DestinationId = fromId };
                _graph.Add(fromId, fromNode);
            }

            var edge = new GraphEdge
            {
                From = fromId,
                To = toId,
                CompanyRoute = route
            };

            fromNode.Edges.Add(edge);
        }
    }

    public Dictionary<Guid, GraphNode> GetGraph()
    {
        return _graph;
    }
}