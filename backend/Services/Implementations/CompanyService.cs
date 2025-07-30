using AutoMapper;
using CosmosOdyssey.Data;
using CosmosOdyssey.Dtos.Response;
using CosmosOdyssey.Models;
using CosmosOdyssey.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdyssey.Services.Implementations;

public class CompanyService(AppDbContext context, IMapper mapper) : BaseService(context), ICompanyService
{
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

        return mapper.Map<List<Company>, List<CompanyResponse>>(companies);
    }
}