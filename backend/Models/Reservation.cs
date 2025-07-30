using System.ComponentModel.DataAnnotations;

namespace CosmosOdyssey.Models;

public class Reservation : BaseModel
{
    public Guid Id { get; set; }

    [MaxLength(255)] public required string FirstName { get; set; }

    [MaxLength(255)] public required string LastName { get; set; }

    public required double TotalPrice { get; set; }
    public required double TotalTravelMinutes { get; set; }

    public ICollection<CompanyRoute> CompanyRoutes { get; set; } = new List<CompanyRoute>();
}