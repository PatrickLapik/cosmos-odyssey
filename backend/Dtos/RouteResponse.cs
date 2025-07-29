namespace CosmosOdyssey.Dtos;

public class RouteResponse
{
    public Guid Id { get; set; }
    public string From  { get; set; }
    public Guid FromId { get; set; }
    public string To { get; set; }
    public Guid ToId { get; set; }
}