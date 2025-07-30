namespace CosmosOdyssey.Models;

public class TravelPrice : BaseModel
{
    public Guid Id { get; set; }
    public DateTime ValidUntil { get; set; }

    public ICollection<CompanyRoute> CompanyRoutes { get; set; } = new List<CompanyRoute>();
}