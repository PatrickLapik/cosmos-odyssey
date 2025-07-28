using CosmosOdyssey.Models;

namespace CosmosOdyssey.Services;

public interface IApiLogService
{
    public Task<ApiLog?> GetLatest();
    public Task Save(ApiLog log);
}