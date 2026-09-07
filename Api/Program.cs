using Api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDependencies(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

await MigrateDatabaseAsync(app.Services);

app.Run();

static async Task MigrateDatabaseAsync(IServiceProvider serviceProvider)
{
  using var serviceScope = serviceProvider.CreateScope();

  await serviceScope
    .ServiceProvider.GetRequiredService<AppDbContext>()
    .Database.MigrateAsync();
}
