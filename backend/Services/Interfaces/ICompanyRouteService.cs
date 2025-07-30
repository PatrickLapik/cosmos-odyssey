using CosmosOdyssey.Dtos.Request;
using CosmosOdyssey.Dtos.Response;
using CosmosOdyssey.Models;
using Pagination.EntityFrameworkCore.Extensions;

namespace CosmosOdyssey.Services.Interfaces;

public interface ICompanyRouteService
{
    public Task Save(CompanyRoute route);
    public Task<Pagination<CompanyRouteResponse>> GetPaginated(int page = 1);
    public List<FullCompanyRoutesResponse> GetAllRoutes(RouteRequest request);
}