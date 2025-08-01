using CosmosOdyssey.Services.Interfaces;

namespace CosmosOdyssey.Services.Implementations;

public class ExternalPriceListHostedService(ILogger<ExternalPriceListHostedService> logger, IServiceProvider services)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Price List Api Hosted Service is starting...");
        await DoWork(stoppingToken);
    }

    private async Task DoWork(CancellationToken stoppingToken)
    {
        logger.LogInformation("Price List Api Hosted Service is working...");

        using var scope = services.CreateScope();
        var externalPriceListService = scope.ServiceProvider.GetRequiredService<IExternalPriceListService>();
        var apiLogService = scope.ServiceProvider.GetRequiredService<IApiLogService>();
        var graphBuilderService = scope.ServiceProvider.GetRequiredService<IGraphBuilderService>();

        while (!stoppingToken.IsCancellationRequested)
        {
            DateTime? validUntil = null;

            try
            {
                var latestLog = await apiLogService.GetLatest();

                if (latestLog != null) validUntil = latestLog.ExternalPriceList.ValidUntil;

                if (validUntil == null || validUntil <= DateTime.UtcNow)
                {
                    try
                    {
                        var priceList = await externalPriceListService.FetchPriceList();
                        await externalPriceListService.SavePriceList(priceList);
                        validUntil = priceList.ValidUntil;
                    }
                    catch (HttpRequestException netEx)
                    {
                        logger.LogWarning("Failed to fetch price list from API. Falling back to latest saved list. \nDetails: {ex}", netEx.Message);

                        latestLog = await apiLogService.GetLatest();
                        validUntil = latestLog?.ExternalPriceList.ValidUntil;

                        await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);

                        if (validUntil == null)
                        {
                            logger.LogError("No valid saved price list available after network failure.");
                            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
                            continue;
                        }
                    }
                }

                await graphBuilderService.LoadGraph();

                var delay = validUntil.Value - DateTime.UtcNow;

                if (delay <= TimeSpan.Zero) delay = TimeSpan.Zero;

                await Task.Delay(delay, stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogInformation("Price List Api Hosted Service stopping due to an Error \n Error details: {ex}",
                    ex.Message);
            }
        }
    }
}
