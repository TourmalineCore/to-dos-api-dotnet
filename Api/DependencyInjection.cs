using Microsoft.EntityFrameworkCore;

namespace Api;

public static class DependencyInjection
{
  private const string DefaultConnection = "DefaultConnection";

  public static void AddDependencies(
    this IServiceCollection services,
    IConfiguration configuration
  )
  {
    var connectionString = configuration.GetConnectionString(
      DefaultConnection
    );

    services.AddDbContext<AppDbContext>(options =>
    {
      options.UseNpgsql(connectionString);
    });
  }
}
