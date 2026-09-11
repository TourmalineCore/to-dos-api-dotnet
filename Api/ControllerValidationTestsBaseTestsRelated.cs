using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Xunit;

namespace Api;

public class ControllerValidationTestsBase(
  WebApplicationFactory<Program> factory
) : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
{
  protected HttpClient HttpClient = null!;

  public async Task InitializeAsync()
  {
    factory = factory.WithWebHostBuilder(builder =>
    {
      builder.ConfigureTestServices(services =>
      {
        // https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-10.0&pivots=xunit#customize-webapplicationfactory
        var dbContextDescriptor = services.SingleOrDefault(d =>
          d.ServiceType
          == typeof(IDbContextOptionsConfiguration<AppDbContext>)
        )!;

        services.Remove(dbContextDescriptor);

        services.AddDbContext<AppDbContext>(options =>
          options.UseInMemoryDatabase(
            databaseName: new Random().Next().ToString(),
            x => x.EnableNullChecks(false)
          )
        );
      });
    });

    HttpClient = factory.CreateClient();
  }

  public async Task DisposeAsync()
  {
    HttpClient.Dispose();
    await factory.DisposeAsync();
  }
}
