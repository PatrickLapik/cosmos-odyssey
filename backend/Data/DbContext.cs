using Microsoft.EntityFrameworkCore;
using CosmosOdyssey.Models;

namespace CosmosOdyssey.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public required DbSet<Company> Companies { get; set; }
}