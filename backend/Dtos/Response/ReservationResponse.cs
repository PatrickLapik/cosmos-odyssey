namespace CosmosOdyssey.Dtos.Response;

public class ReservationResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<CompanyRouteResponse> CompanyRouteResponses { get; set; } = [];
    public double TotalPrice { get; set; }
    public double TotalTravelMinutes { get; set; }
}