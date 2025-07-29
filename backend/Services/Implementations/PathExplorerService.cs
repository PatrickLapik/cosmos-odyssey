using CosmosOdyssey.Data;
using CosmosOdyssey.Models;
using CosmosOdyssey.Services.Graph;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdyssey.Services;

public class PathExplorerService : IPathExplorerService
{
    private readonly IGraphBuilderService _graphBuilderService;

    public PathExplorerService(IGraphBuilderService graphBuilderService)
    {
        _graphBuilderService = graphBuilderService;
    }

    public List<List<CompanyRoute>> FindAllValidPaths(Guid fromId, Guid toId, DateTime startTime)
    {
        var graph = _graphBuilderService.GetGraph();
        var results = new List<List<CompanyRoute>>();
        
        void DFS(Guid current, DateTime currentTime, List<CompanyRoute> path)
        {
            if (!graph.TryGetValue(current, out var value))
                return;

            foreach (var edge in value.Edges)
            {
                var route = edge.CompanyRoute;

                if (route.TravelStart < currentTime)
                    continue;

                var newPath = new List<CompanyRoute>(path) { route };

                if (route.Route!.ToDestinationId == toId)
                {
                    results.Add(newPath);
                }
                else
                {
                    DFS(route.Route.ToDestinationId, route.TravelEnd, newPath);
                }
            }
        }

        DFS(fromId, startTime, []);

        return results;
    } 
}