using System.ComponentModel.DataAnnotations;

namespace CosmosOdyssey.Models;

public class BaseModel 
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
