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
  protected HttpClient _httpClient = null!;

  public async Task InitializeAsync()
  {
    _httpClient = factory.CreateClient();

    factory = factory.WithWebHostBuilder(builder =>
    {
      builder.ConfigureTestServices(services =>
      {
        services.Remove(
          services.Single(x =>
            x.ServiceType
            == typeof(IDbContextOptionsConfiguration<AppDbContext>)
          )
        );

        services.AddDbContext<AppDbContext>(options =>
          options.UseInMemoryDatabase(
            databaseName: new Random().Next().ToString(),
            x => x.EnableNullChecks(false)
          )
        );
      });
    });
  }

  public async Task DisposeAsync()
  {
    _httpClient.Dispose();
    await factory.DisposeAsync();
  }
}
