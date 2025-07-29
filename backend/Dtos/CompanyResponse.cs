using CosmosOdyssey.Models;
using Route = CosmosOdyssey.Models.Route;

namespace CosmosOdyssey.Dtos;

public class CompanyResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}