using AutoMapper;
using AutoMapper.QueryableExtensions;
using CosmosOdyssey.Data;
using CosmosOdyssey.Dtos;
using CosmosOdyssey.Models;
using Microsoft.EntityFrameworkCore;
using Pagination.EntityFrameworkCore.Extensions;
using Route = CosmosOdyssey.Models.Route;

namespace CosmosOdyssey.Services;

public class CompanyRouteService : BaseService, ICompanyRouteService
{
    private readonly IMapper _mapper;
    private readonly IPathExplorerService _pathExplorerService;
    
    public CompanyRouteService(AppDbContext context, IMapper mapper, IPathExplorerService pathExplorerService) : base(context)
    {
        _mapper = mapper;
        _pathExplorerService = pathExplorerService;
    }

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
        
        return _mapper.Map<Pagination<CompanyRouteResponse>>(results);
    }

    public List<FullCompanyRoutesResponse> GetAllRoutes(Guid fromId, Guid toId)
    {
        var routes = _pathExplorerService.FindAllValidPaths(fromId, toId, DateTime.Now);
        
        return routes.ConvertAll(r => _mapper.Map<FullCompanyRoutesResponse>(r));
    }
}