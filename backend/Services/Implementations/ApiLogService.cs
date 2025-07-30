using CosmosOdyssey.Data;
using CosmosOdyssey.Models;
using CosmosOdyssey.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdyssey.Services.Implementations;

public class ApiLogService(AppDbContext context) : BaseService(context), IApiLogService
{
    public async Task Save(ApiLog apiLog)
    {
        await Context.ApiLogs.AddAsync(apiLog);
        await Context.SaveChangesAsync();
    }

    public async Task<ApiLog?> GetLatest()
    {
        return await Context.ApiLogs.OrderByDescending(l => l.CreatedAt).FirstOrDefaultAsync();
    }
}