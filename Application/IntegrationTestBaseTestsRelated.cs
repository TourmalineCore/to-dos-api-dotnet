using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Xunit;

namespace Application;

public class IntegrationTestBase : IAsyncLifetime
{
  private NpgsqlConnection _dbConnection = null!;

  private DbContextOptions<AppDbContext> _dbContextOptions = null!;

  private NpgsqlTransaction _dbTransaction = null!;

  public async Task InitializeAsync()
  {
    var configuration = new ConfigurationBuilder()
      .SetBasePath(
        Path.Combine(Directory.GetCurrentDirectory(), "../../../../Api")
      )
      .AddJsonFile(
        "appsettings.json",
        optional: false,
        reloadOnChange: false
      )
      .AddJsonFile(
        $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
        optional: true
      )
      .AddEnvironmentVariables()
      .Build();

    var connectionString = configuration.GetConnectionString(
      "DefaultConnection"
    );
    _dbConnection = new NpgsqlConnection(connectionString);

    await _dbConnection.OpenAsync();

    await ApplyMigrationsAsync();

    _dbTransaction = await _dbConnection.BeginTransactionAsync(
      System.Data.IsolationLevel.Serializable
    );

    var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

    optionsBuilder.UseNpgsql(_dbConnection);

    _dbContextOptions = optionsBuilder.Options;
  }

  // Rollback Transaction And Close Db Connection
  public async Task DisposeAsync()
  {
    if (_dbTransaction != null)
    {
      try
      {
        await _dbTransaction.RollbackAsync();
      }
      finally
      {
        await _dbTransaction.DisposeAsync();
      }
    }

    if (_dbConnection != null)
    {
      await _dbConnection.CloseAsync();
    }
  }

  private async Task ApplyMigrationsAsync()
  {
    var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
    optionsBuilder.UseNpgsql(_dbConnection);

    using var context = new AppDbContext(optionsBuilder.Options);

    var pendingMigrations =
      await context.Database.GetPendingMigrationsAsync();
    if (pendingMigrations.Any())
    {
      await context.Database.MigrateAsync();
    }
  }

  protected AppDbContext CreateAppDbContext()
  {
    var context = new AppDbContext(_dbContextOptions);

    // This avoids errors in the tests. System.InvalidOperationException : A transaction is already in progress; nested/concurrent transactions aren't supported.
    context.Database.UseTransaction(_dbTransaction);

    return context;
  }

  protected async Task<TEntity> AddEntityAndSaveAsync<TEntity>(
    AppDbContext context,
    TEntity newEntity
  )
    where TEntity : class
  {
    await context.Set<TEntity>().AddAsync(newEntity);

    await context.SaveChangesAsync();

    return newEntity;
  }
}
