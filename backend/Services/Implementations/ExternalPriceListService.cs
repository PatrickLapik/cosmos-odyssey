using System.Text.Json;
using CosmosOdyssey.Data;
using CosmosOdyssey.Models;
using CosmosOdyssey.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Route = CosmosOdyssey.Models.Route;

namespace CosmosOdyssey.Services.Implementations;

public class ExternalPriceListService : BaseService, IExternalPriceListService
{
    private readonly IApiLogService _apiLogService;
    private readonly ICompanyRouteService _companyRouteService;
    private readonly ICompanyService _companyService;
    private readonly IDestinationService _destinationService;
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    private readonly IRouteService _routeService;
    private readonly ITravelPriceService _travelPriceService;

    public ExternalPriceListService(HttpClient httpClient, IApiLogService apiLogService,
        IDestinationService destinationService, IRouteService routeService, ICompanyService companyService,
        ITravelPriceService travelPriceService, ICompanyRouteService companyRouteService,
        AppDbContext context) : base(context)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://cosmosodyssey.azurewebsites.net/api/v1.0/");
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _apiLogService = apiLogService;
        _routeService = routeService;
        _companyService = companyService;
        _travelPriceService = travelPriceService;
        _companyRouteService = companyRouteService;
        _destinationService = destinationService;
    }

    public async Task CleanUpPriceList()
    {
        var totalCount = await Context.TravelPrices.CountAsync();

        if (totalCount > 15)
        {
            var toDelete = await Context.TravelPrices
                .OrderBy(tp => tp.CreatedAt)
                .Take(totalCount - 15)
                .ToListAsync();
            
            Context.TravelPrices.RemoveRange(toDelete);
            await Context.SaveChangesAsync();

            var orphanedReservations = await Context.Reservations
                .Where(r => !r.CompanyRoutes.Any())
                .ToListAsync();
            
            Context.Reservations.RemoveRange(orphanedReservations);
            await Context.SaveChangesAsync();
        }
    }

    public async Task SavePriceList(ExternalPriceList priceList)
    {
        await SavePriceListToLogs(priceList);

        await NormalizePriceList(priceList);

        await CleanUpPriceList();
    }

    public async Task<ExternalPriceList> FetchPriceList()
    {
        var response = await _httpClient.GetAsync("TravelPrices");

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        var priceList = JsonSerializer.Deserialize<ExternalPriceList>(content, _jsonSerializerOptions);

        if (priceList == null) throw new Exception("Unable to deserialize");

        return priceList;
    }

    private async Task NormalizePriceList(ExternalPriceList priceList)
    {
        var legs = priceList.Legs;

        await SyncDestinations(legs);
        await SyncRoutes(legs);
        await SyncCompanies(legs);

        var travelPrice = new TravelPrice
        {
            ValidUntil = priceList.ValidUntil
        };

        await _travelPriceService.Save(travelPrice);

        var allCompanies = await _companyService.GetAll();
        var allRoutes = await _routeService.GetAll();

        foreach (var leg in priceList.Legs)
        foreach (var provider in leg.Providers)
        {
            var companyRoute = new CompanyRoute
            {
                TravelStart = provider.FlightStart,
                TravelEnd = provider.FlightEnd,
                CompanyId = allCompanies.FirstOrDefault(c => c.Name == provider.Company.Name)!.Id,
                RouteId = allRoutes.FirstOrDefault(r =>
                        r.ToDestination.Name == leg.RouteInfo.To.Name &&
                        r.FromDestination.Name == leg.RouteInfo.From.Name)
                    .Id,
                TravelPriceId = travelPrice.Id,
                Price = provider.Price
            };

            await _companyRouteService.Save(companyRoute);
        }
    }

    private async Task SyncDestinations(List<Leg> legs)
    {
        var allDestinationNames = legs
            .SelectMany(l => new[] { l.RouteInfo.From.Name, l.RouteInfo.To.Name })
            .Distinct()
            .ToList();

        foreach (var destinationName in allDestinationNames)
        {
            var newDestination = new Destination
            {
                Name = destinationName
            };

            await _destinationService.Save(newDestination);
        }
    }

    private async Task SyncRoutes(List<Leg> legs)
    {
        var allRouteNames = legs
            .Select(l => new
            {
                From = l.RouteInfo.From.Name,
                To = l.RouteInfo.To.Name,
                l.RouteInfo.Distance
            })
            .Distinct()
            .ToList();

        var allDestinations = await Context.Destinations.ToListAsync();

        foreach (var routeNames in allRouteNames)
        {
            var newRoute = new Route
            {
                FromDestinationId = allDestinations.FirstOrDefault(d => d.Name == routeNames.From)!.Id,
                ToDestinationId = allDestinations.FirstOrDefault(d => d.Name == routeNames.To)!.Id,
                Distance = routeNames.Distance
            };

            await _routeService.Save(newRoute);
        }
    }

    private async Task SyncCompanies(List<Leg> legs)
    {
        var allCompanyNames = legs
            .SelectMany(l => l.Providers)
            .Select(p => p.Company.Name)
            .Distinct()
            .ToList();

        foreach (var companyName in allCompanyNames)
        {
            var newCompany = new Company
            {
                Name = companyName
            };
            await _companyService.Save(newCompany);
        }
    }

    private async Task SavePriceListToLogs(ExternalPriceList priceList)
    {
        var apiLog = new ApiLog { ExternalPriceList = priceList };

        await _apiLogService.Save(apiLog);
    }
}