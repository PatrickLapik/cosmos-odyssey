namespace CosmosOdyssey.Models;

public class Route : BaseModel
{
    public Guid Id { get; set; }
    public long Distance { get; set; }
    
    public Guid ToDestinationId { get; set; }
    public Destination? ToDestination { get; set; }
    
    public Guid FromDestinationId { get; set; }
    public Destination? FromDestination { get; set; }
    
    public ICollection<CompanyRoute> CompanyRoutes { get; set; } = new List<CompanyRoute>();
}