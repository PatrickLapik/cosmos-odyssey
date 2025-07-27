namespace CosmosOdyssey.Models;

public class Route : BaseModel
{
    public Guid Id { get; set; }
    public long Distance { get; set; }
    
    public required Guid ToDestinationId { get; set; }
    public required Destination ToDestination { get; set; }
    
    public required Guid FromDestinationId { get; set; }
    public required Destination FromDestination { get; set; }
    
    public ICollection<CompanyRoute> CompanyRoutes { get; set; } = new List<CompanyRoute>();
}