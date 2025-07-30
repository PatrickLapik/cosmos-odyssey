namespace CosmosOdyssey.Services.Implementations.Graph;

public class GraphNode
{
    public Guid DestinationId { get; set; }
    public List<GraphEdge> Edges { get; set; } = [];
}