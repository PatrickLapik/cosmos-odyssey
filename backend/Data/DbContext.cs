using Microsoft.EntityFrameworkCore;
using CosmosOdyssey.Models;
using Route = CosmosOdyssey.Models.Route;

namespace CosmosOdyssey.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Route>()
            .HasIndex(r => new { r.FromDestinationId, r.ToDestinationId })
            .IsUnique();
        
        modelBuilder.Entity<Route>()
            .HasOne(r => r.FromDestination)
            .WithMany(d => d.RoutesFromHere)
            .HasForeignKey(r => r.FromDestinationId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Route>()
            .HasOne(r => r.ToDestination)
            .WithMany(d => d.RoutesToHere)
            .HasForeignKey(r => r.ToDestinationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ApiLog>().Property(e => e.ExternalPriceList).HasColumnType("json");
    }

    public required DbSet<Company> Companies { get; set; }
    public required DbSet<Destination> Destinations { get; set; }
    public required DbSet<Route> Routes { get; set; }
    public required DbSet<Reservation> Reservations { get; set; }
    public required DbSet<CompanyRoute> CompanyRoutes { get; set; }
    public required DbSet<TravelPrice> TravelPrices { get; set; }
    public required DbSet<ApiLog> ApiLogs { get; set; }
    
    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<BaseModel>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = now;
                entry.Entity.UpdatedAt = now;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Property(e => e.CreatedAt).IsModified = false;
                entry.Entity.UpdatedAt = now;
            }
        }
    }
}