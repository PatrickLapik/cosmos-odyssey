using System.ComponentModel.DataAnnotations.Schema;

namespace CosmosOdyssey.Models;

public class ExternalPriceList
{
    public Guid Id { get; set; } 
    public DateTime ValidUntil { get; set; }
    public List<Leg> Legs { get; set; } = [];
}

public class Leg
{
   public Guid Id { get; set; }
   public required RouteInfo RouteInfo { get; set; }
   public List<Provider> Providers { get; set; } = [];
}

public class RouteInfo
{
    public Guid Id { get; set; }
    public required Destination From { get; set; }
    public required Destination To { get; set; }
    public long Distance { get; set; }
}

public class Provider
{
    public Guid Id { get; set; }
    public required CompanyInfo Company { get; set; }
    public double Price { get; set; }
    public DateTime FlightStart { get; set; }
    public DateTime FlightEnd { get; set; }
}

public class DestinationInfo
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}

public class CompanyInfo
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}