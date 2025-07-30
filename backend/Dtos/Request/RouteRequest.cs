namespace CosmosOdyssey.Dtos;

public class RouteRequest
{
    public Guid FromId { get; set; }
    public Guid ToId { get; set; }
    public Guid CompanyId { get; set; }
    public double MaxPrice { get; set; }
    public long MaxTravelMinutes { get; set; }
    public long MaxDistance { get; set; }
}