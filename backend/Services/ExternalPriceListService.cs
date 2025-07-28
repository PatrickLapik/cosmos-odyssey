using System.Text.Json;
using CosmosOdyssey.Data;
using CosmosOdyssey.Models;

namespace CosmosOdyssey.Services;

public class ExternalPriceListService : IExternalPriceListService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly IApiLogService _apiLogService;

    public ExternalPriceListService(HttpClient httpClient, AppDbContext context, IApiLogService apiLogService)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://cosmosodyssey.azurewebsites.net/api/v1.0/");
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _apiLogService = apiLogService;
    }

    public async Task<ExternalPriceList> GetPriceList()
    {
        var response = await _httpClient.GetAsync("TravelPrices");
        
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        
        var priceList = JsonSerializer.Deserialize<ExternalPriceList>(content, _jsonSerializerOptions);
        
        if (priceList == null)
        {
            throw new Exception("Unable to deserialize");
        }
        
        await SavePriceListToLogs(priceList);
        
        return priceList;
    }

    private async Task SavePriceListToLogs(ExternalPriceList priceList)
    {
        var apiLog = new ApiLog { ExternalPriceList = priceList };
    
        await _apiLogService.Save(apiLog); 
    }
}