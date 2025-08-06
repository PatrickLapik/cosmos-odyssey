using CosmosOdyssey.Data;
using CosmosOdyssey.Extensions;
using CosmosOdyssey.Services;
using CosmosOdyssey.Services.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration["ConnectionStrings:DatabaseConnection"];
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
        optionsBuilder => optionsBuilder.UseMicrosoftJson()));

builder.Services.AddDependencyGroup(builder.Configuration);

builder.Services.AddHostedService<ExternalPriceListHostedService>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) 
{
    app.MapOpenApi();
    
    var db = app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
    
    var pendingMigrations = await db.Database.GetPendingMigrationsAsync();
    
    if (pendingMigrations.Any())
        await db.Database.MigrateAsync();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
