using CosmosOdyssey.Models;

namespace CosmosOdyssey.Services;

public class ExternalPriceListHostedService : BackgroundService
{
    private readonly ILogger<ExternalPriceListHostedService> _logger;
    private readonly ExternalPriceListService _service;

    public ExternalPriceListHostedService(ILogger<ExternalPriceListHostedService> logger, ExternalPriceListService service)
    {
        _logger = logger;
        _service = service;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Price List Api Hosted Service is starting...");
        await DoWork(stoppingToken);
    }

    private async Task DoWork(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Price List Api Hosted Service is working...");

        while (!stoppingToken.IsCancellationRequested)
        {
            DateTime? validUntil = null;
            ExternalPriceList? priceList = null;

            try
            {
                if (validUntil == null)
                {
                    priceList = await _service.GetPriceList();
                    validUntil = priceList.ValidUntil;
                }
                
                var delay = validUntil.Value - DateTime.UtcNow;

                if (delay <= TimeSpan.Zero)
                {
                    delay = TimeSpan.Zero;
                }
                
                await Task.Delay(delay, stoppingToken);
                
                // Do task
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Price List Api Hosted Service stopping due to an Error \n Error details: {ex}", ex.Message);
            }
        }
    }
}