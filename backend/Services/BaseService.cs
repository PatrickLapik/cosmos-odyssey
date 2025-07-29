using CosmosOdyssey.Data;
using CosmosOdyssey.Models;
using Microsoft.EntityFrameworkCore;

namespace CosmosOdyssey.Services;

public class BaseService
{
    protected readonly AppDbContext Context;

    protected BaseService(AppDbContext context)
    {
        Context = context; 
    }
}