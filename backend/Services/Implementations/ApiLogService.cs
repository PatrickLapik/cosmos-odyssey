using CosmosOdyssey.Data;
using CosmosOdyssey.Models;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdyssey.Services;

public class ApiLogService : BaseService, IApiLogService
{
    public ApiLogService(AppDbContext context) : base(context)
    {
    }

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