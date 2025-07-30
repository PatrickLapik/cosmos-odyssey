using AutoMapper;
using CosmosOdyssey.Data;
using CosmosOdyssey.Dtos.Request;
using CosmosOdyssey.Dtos.Response;
using CosmosOdyssey.Models;
using CosmosOdyssey.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Pagination.EntityFrameworkCore.Extensions;

namespace CosmosOdyssey.Services.Implementations;

public class CompanyRouteService(AppDbContext context, IMapper mapper, IPathExplorerService pathExplorerService)
    : BaseService(context), ICompanyRouteService
{
    public async Task Save(CompanyRoute route)
    {
        var existing = await Context.Destinations.AnyAsync(d => d.Id == route.Id);
        if (existing) return;

        await Context.CompanyRoutes.AddAsync(route);
        await Context.SaveChangesAsync();
    }

    public async Task<Pagination<CompanyRouteResponse>> GetPaginated(int page = 1)
    {
        var results = await Context.CompanyRoutes
            .Include(cr => cr.Company)
            .Include(cr => cr.Route)
            .ThenInclude(r => r.FromDestination)
            .Include(cr => cr.Route)
            .ThenInclude(r => r.ToDestination)
            .Include(cr => cr.TravelPrice)
            .AsPaginationAsync(page, PageSize);

        return mapper.Map<Pagination<CompanyRouteResponse>>(results);
    }

    public List<FullCompanyRoutesResponse> GetAllRoutes(RouteRequest request)
    {
        var routes = pathExplorerService.FindAllValidPaths(request.FromId, request.ToId, DateTime.Now);
        var mappedRoutes = routes.ConvertAll(r => mapper.Map<FullCompanyRoutesResponse>(r));

        return mappedRoutes
            .Where(route =>
                (request.CompanyId == Guid.Empty ||
                 route.CompanyRouteResponses.TrueForAll(cp => cp.Company.Id == request.CompanyId)) &&
                (request.MaxPrice <= 0 || route.TotalPrice <= request.MaxPrice) &&
                (request.MaxDistance <= 0 || route.TotalDistance <= request.MaxDistance) &&
                (request.MaxTravelMinutes <= 0 || route.TotalTravelMinutes <= request.MaxTravelMinutes)
            ).ToList();
    }
}