using CosmosOdyssey.Models;

namespace CosmosOdyssey.Services.Interfaces;

public interface IExternalPriceListService
{
    public Task SavePriceList(ExternalPriceList priceList);
    public Task CleanUpPriceList();
    public Task<ExternalPriceList> FetchPriceList();
}