using CosmosOdyssey.Data;
using CosmosOdyssey.Models;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdyssey.Services;

public class CompanyService : BaseService, ICompanyService
{
    public CompanyService(AppDbContext context) : base(context)
    {
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

    public async Task<List<Company>> GetAll()
    {
        return await Context.Companies.ToListAsync();
    }
}