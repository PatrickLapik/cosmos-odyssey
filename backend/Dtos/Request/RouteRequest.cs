using System.Text.Json.Serialization;

namespace CosmosOdyssey.Dtos.Request;

public enum RouteSortBy
{
    None,
    Price,
    TravelTime,
    Distance
}

public enum SortOrder
{
    Asc,
    Desc
}

public class RouteRequest
{
    public Guid FromId { get; set; }
    public Guid ToId { get; set; }
    public Guid CompanyId { get; set; }
    public double MaxPrice { get; set; }
    public long MaxTravelMinutes { get; set; }
    public long MaxDistance { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public RouteSortBy SortBy { get; set; } = RouteSortBy.None;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SortOrder SortOrder { get; set; } = SortOrder.Asc;
}