using CosmosOdyssey.Data;
using CosmosOdyssey.Models;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdyssey.Services.Graph;

public class GraphBuilderService : IGraphBuilderService
{
    private Dictionary<Guid, GraphNode> _graph;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public GraphBuilderService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _graph = new Dictionary<Guid, GraphNode>();
    }

    public async Task LoadGraph()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetService<AppDbContext>();
        
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
    
    public Dictionary<Guid, GraphNode> GetGraph() =>  _graph;
}