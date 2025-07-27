using System.ComponentModel.DataAnnotations;

namespace CosmosOdyssey.Models;

public class CompanyRoute : BaseModel
{
    public Guid Id { get; set; }
    
    [MaxLength(1020)]
    public required string Description { get; set; }
    public required DateTime TravelStart { get; set; }
    public required DateTime TravelEnd { get; set; }
    
    public Guid CompanyId { get; set; }
    public required Company Company { get; set; } 
    
    public Guid RouteId { get; set; }
    public required Route Route { get; set; }
    
    public Guid TravelPriceId { get; set; }
    public required TravelPrice TravelPrice { get; set; }
    
    public ICollection<Reservation> Reservations { get; } = new List<Reservation>();
}