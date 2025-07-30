using CosmosOdyssey.Dtos;
using CosmosOdyssey.Models;
using Pagination.EntityFrameworkCore.Extensions;

namespace CosmosOdyssey.Services;

public interface ICompanyRouteService
{
    public Task Save(CompanyRoute route);
    public Task<Pagination<CompanyRouteResponse>> GetPaginated(int page = 1);
    public List<FullCompanyRoutesResponse> GetAllRoutes(RouteRequest request);
}