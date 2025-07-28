using CosmosOdyssey.Data;
using CosmosOdyssey.Services;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdyssey.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDependencyGroup(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddOpenApi();
        services.AddHttpClient();
        
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DataBaseConnection");
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), optionsBuilder => optionsBuilder.UseMicrosoftJson()));

        services.AddScoped<IApiLogService, ApiLogService>();
        services.AddScoped<IExternalPriceListService, ExternalPriceListService>();
        
        return services;
    }
}
