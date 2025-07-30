using CosmosOdyssey.Services.Implementations.Graph;

namespace CosmosOdyssey.Services.Interfaces;

public interface IGraphBuilderService
{
    public Task LoadGraph();
    public Dictionary<Guid, GraphNode> GetGraph();
}