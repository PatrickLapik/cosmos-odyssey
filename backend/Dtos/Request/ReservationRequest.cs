namespace CosmosOdyssey.Dtos.Request;

public class ReservationRequest
{
    public List<Guid> CompanyRouteIds { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}