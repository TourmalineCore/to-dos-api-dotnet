using Application.Features.ToDos.Handlers.CreateToDo;
using Application.Features.ToDos.Handlers.DeleteToDo;
using Application.Features.ToDos.Handlers.GetToDos;
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

    services.AddTransient<CreateToDoHandler>();
    services.AddTransient<CreateToDoCommand>();

    services.AddTransient<GetToDosHandler>();
    services.AddTransient<GetToDosQuery>();

    services.AddTransient<DeleteToDoHandler>();
    services.AddTransient<DeleteToDoCommand>();
  }
}
