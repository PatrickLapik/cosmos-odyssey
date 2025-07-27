using System.Text.Json;
using CosmosOdyssey.Models;

namespace CosmosOdyssey.Services;

public class ExternalPriceListService 
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public ExternalPriceListService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://cosmosodyssey.azurewebsites.net/api/v1.0/");
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<ExternalPriceList> GetPriceList()
    {
        var response = await _httpClient.GetAsync("TravelPrices");
        
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        
        Console.WriteLine(content);
        
        var priceList = JsonSerializer.Deserialize<ExternalPriceList>(content, _jsonSerializerOptions);
        
        return priceList;
    }
}