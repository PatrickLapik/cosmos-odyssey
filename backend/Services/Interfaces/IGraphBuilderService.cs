namespace CosmosOdyssey.Services.Graph;

public interface IGraphBuilderService
{
    public Task LoadGraph();
    public Dictionary<Guid, GraphNode> GetGraph();
}