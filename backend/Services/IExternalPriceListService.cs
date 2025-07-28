using CosmosOdyssey.Models;

namespace CosmosOdyssey.Services;

public interface IExternalPriceListService
{
    public Task<ExternalPriceList> GetPriceList();
}