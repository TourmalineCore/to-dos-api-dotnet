using System.Text.Json.Serialization;
using Api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.

builder
  .Services.AddControllers()
  .AddJsonOptions(options =>
  {
    options.JsonSerializerOptions.Converters.Add(
      new JsonStringEnumConverter()
    );
  });
;

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

  using var context =
    serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

  if (!context.Database.IsInMemory())
  {
    await context.Database.MigrateAsync();
  }
}
