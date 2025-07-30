namespace CosmosOdyssey.Dtos;

public class ReservationResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<RouteResponse> CompanyRoutes { get; set; }
    public double TotalPrice { get; set; }
    public double TotalTravelMinutes { get; set; }
    public List<string> CompanyNames { get; set; }
}