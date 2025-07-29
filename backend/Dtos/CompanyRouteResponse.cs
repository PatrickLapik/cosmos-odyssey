using CosmosOdyssey.Models;
using Route = CosmosOdyssey.Models.Route;

namespace CosmosOdyssey.Dtos;

public class CompanyRouteResponse
{
    public Guid Id { get; set; }
    public DateTime TravelStart { get; set; }
    public DateTime TravelEnd { get; set; }
    public double Price { get; set; }
    public CompanyResponse Company { get; set; }
    public RouteResponse Route { get; set; } 
    public DateTime ValidUntil { get; set; }
}