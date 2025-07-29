using CosmosOdyssey.Models;

namespace CosmosOdyssey.Services;

public class ExternalPriceListHostedService : BackgroundService
{
    private readonly ILogger<ExternalPriceListHostedService> _logger;
    private readonly IServiceProvider _services;

    public ExternalPriceListHostedService(ILogger<ExternalPriceListHostedService> logger, IServiceProvider services)
    {
        _logger = logger;
        _services = services;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Price List Api Hosted Service is starting...");
        await DoWork(stoppingToken);
    }

    private async Task DoWork(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Price List Api Hosted Service is working...");

        using var scope = _services.CreateScope();
        var externalPriceListService = scope.ServiceProvider.GetRequiredService<IExternalPriceListService>();
        var apiLogService = scope.ServiceProvider.GetRequiredService<IApiLogService>();
            
        while (!stoppingToken.IsCancellationRequested)
        {
            DateTime? validUntil = null;
            
            try
            {
                var latestLog = await apiLogService.GetLatest();

                if (latestLog != null)
                {
                    validUntil = latestLog.ExternalPriceList.ValidUntil;
                }
                
                if (validUntil == null || validUntil <= DateTime.UtcNow)
                {
                    var priceList = await externalPriceListService.FetchPriceList();
                    await externalPriceListService.SavePriceList(priceList);
                    validUntil = priceList.ValidUntil;
                }
                
                var delay = validUntil.Value - DateTime.UtcNow;

                if (delay <= TimeSpan.Zero)
                {
                    delay = TimeSpan.Zero;
                }
                
                await Task.Delay(delay, stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Price List Api Hosted Service stopping due to an Error \n Error details: {ex}", ex.Message);
            }
        }
    }
}