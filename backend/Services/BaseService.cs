using CosmosOdyssey.Data;

namespace CosmosOdyssey.Services;

public class BaseService
{
    protected readonly AppDbContext Context;

    protected int PageSize = 20;

    protected BaseService(AppDbContext context)
    {
        Context = context;
    }
}