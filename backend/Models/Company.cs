using System.ComponentModel.DataAnnotations;

namespace CosmosOdyssey.Models;

public class Company : BaseModel
{
    public Guid Id { get; set; }

    [MaxLength(255)] public required string Name { get; set; }

    public ICollection<CompanyRoute> CompanyRoutes { get; set; } = new List<CompanyRoute>();
}