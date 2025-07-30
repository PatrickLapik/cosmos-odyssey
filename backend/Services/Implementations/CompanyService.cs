using AutoMapper;
using CosmosOdyssey.Data;
using CosmosOdyssey.Dtos;
using CosmosOdyssey.Models;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdyssey.Services;

public class CompanyService : BaseService, ICompanyService
{
    private readonly IMapper _mapper;

    public CompanyService(AppDbContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;
    }

    public async Task Save(Company company)
    {
        var existing = await Context.Companies.AnyAsync(c => c.Name == company.Name);
        if (existing) return;

        await Context.Companies.AddAsync(company);
        await Context.SaveChangesAsync();
    }

    public async Task<Company?> Get(Guid id)
    {
        return await Context.Companies.FindAsync(id);
    }

    public async Task<List<CompanyResponse>> GetAll()
    {
        var companies = await Context.Companies.ToListAsync();

        return _mapper.Map<List<Company>, List<CompanyResponse>>(companies);
    }
}