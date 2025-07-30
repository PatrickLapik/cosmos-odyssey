using CosmosOdyssey.Models;

namespace CosmosOdyssey.Services.Implementations.Graph;

public class GraphEdge
{
    public Guid From { get; set; }
    public Guid To { get; set; }
    public CompanyRoute CompanyRoute { get; set; }
}