using CosmosOdyssey.Data;
using CosmosOdyssey.Extensions;
using CosmosOdyssey.Services;
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();