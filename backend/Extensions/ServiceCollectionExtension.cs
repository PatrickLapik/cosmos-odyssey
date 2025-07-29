using CosmosOdyssey.Data;
using CosmosOdyssey.Services;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdyssey.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDependencyGroup(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddOpenApi();
        services.AddHttpClient();
        

        services.AddScoped<IApiLogService, ApiLogService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ICompanyRouteService, CompanyRouteService>();
        services.AddScoped<IRouteService, RouteService>();
        services.AddScoped<ITravelPriceService, TravePriceService>();
        services.AddScoped<IDestinationService, DestinationService>();
        services.AddScoped<IExternalPriceListService, ExternalPriceListService>();
        
        return services;
    }
}
