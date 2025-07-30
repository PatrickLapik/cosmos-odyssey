using CosmosOdyssey.Dtos;
using CosmosOdyssey.Dtos.Request;
using CosmosOdyssey.Mappings;
using CosmosOdyssey.Services;
using CosmosOdyssey.Services.Implementations;
using CosmosOdyssey.Services.Implementations.Graph;
using CosmosOdyssey.Services.Interfaces;
using CosmosOdyssey.Validators;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace CosmosOdyssey.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDependencyGroup(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddOpenApi();
        services.AddHttpClient();
        services.AddFluentValidationAutoValidation();

        services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

        services.AddScoped<IValidator<RouteRequest>, RouteRequestValidator>();
        services.AddScoped<IValidator<ReservationRequest>, ReservationRequestValidator>();

        services.AddSingleton<IGraphBuilderService, GraphBuilderService>();
        services.AddScoped<IPathExplorerService, PathExplorerService>();
        services.AddScoped<IApiLogService, ApiLogService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ICompanyRouteService, CompanyRouteService>();
        services.AddScoped<IRouteService, RouteService>();
        services.AddScoped<ITravelPriceService, TravePriceService>();
        services.AddScoped<IDestinationService, DestinationService>();
        services.AddScoped<IExternalPriceListService, ExternalPriceListService>();
        services.AddScoped<IReservationService, ReservationService>();

        return services;
    }
}