using System.ComponentModel.DataAnnotations;

namespace CosmosOdyssey.Models;

public class Destination : BaseModel
{
    public Guid Id { get; set; }
    
    [MaxLength(255)]
    public required string Name { get; set; }
    
    [MaxLength(1020)]
    public string? Description { get; set; }
    
    public ICollection<Route> RoutesFromHere { get; set; } = new List<Route>();
    public ICollection<Route> RoutesToHere { get; set; } =  new List<Route>();
}