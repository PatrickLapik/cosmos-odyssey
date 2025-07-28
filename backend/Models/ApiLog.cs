using System.ComponentModel.DataAnnotations;

namespace CosmosOdyssey.Models;

public class ApiLog : BaseModel
{
    public Guid Id { get; set; }
    
    public required ExternalPriceList ExternalPriceList { get; set; }
}
