using CosmosOdyssey.Models;

namespace CosmosOdyssey.Services;

public interface ICompanyRouteService
{
    public Task Save(CompanyRoute route);
}