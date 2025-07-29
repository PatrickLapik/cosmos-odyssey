using System.ComponentModel.DataAnnotations;

namespace CosmosOdyssey.Models;

public class CompanyRoute : BaseModel
{
    public Guid Id { get; set; }
    
    public required DateTime TravelStart { get; set; }
    public required DateTime TravelEnd { get; set; }
    
    public Guid CompanyId { get; set; }
    public Company? Company { get; set; } 
    
    public Guid RouteId { get; set; }
    public Route? Route { get; set; }
    
    public required Guid TravelPriceId { get; set; }
    public TravelPrice? TravelPrice { get; set; }
    
    public required double Price {  get; set; }
    
    public ICollection<Reservation> Reservations { get; } = new List<Reservation>();
}