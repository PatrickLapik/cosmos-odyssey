using CosmosOdyssey.Models;
using Route = CosmosOdyssey.Models.Route;

namespace CosmosOdyssey.Dtos;

public class ReservationRequest
{
    public List<Guid> CompanyRouteIds { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}