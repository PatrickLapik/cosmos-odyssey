using CosmosOdyssey.Data;
using CosmosOdyssey.Models;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdyssey.Services;

public class ApiLogService : IApiLogService
{
    private readonly AppDbContext _context;

    public ApiLogService(AppDbContext context)
    {
        _context = context;
    }

    public async Task Save(ApiLog apiLog)
    {
        _context.ApiLogs.Add(apiLog);
        await _context.SaveChangesAsync();
    }

    public async Task<ApiLog?> GetLatest()
    {
        var log = await _context.ApiLogs.OrderByDescending(l => l.CreatedAt).FirstOrDefaultAsync();
        return log;
    }
}