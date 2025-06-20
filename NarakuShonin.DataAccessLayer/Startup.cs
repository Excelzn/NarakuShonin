using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NarakuShonin.DataAccessLayer.Repositories;
using NarakuShonin.Shared.Repositories;

namespace NarakuShonin.DataAccessLayer;

public static class Startup
{
  public static void AddDataAccessLayer(this IServiceCollection services,
    IConfiguration configuration)
  {
    var connectionString = configuration["NarakuShonin:ConnectionString"];
    services.AddFluentMigratorCore()
      .ConfigureRunner(rb =>
      {
        rb.AddPostgres().WithGlobalConnectionString(connectionString)
          .ScanIn(typeof(Startup).Assembly).For.All();
      }).AddLogging(lb => lb.AddFluentMigratorConsole());
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddSingleton<IConnectionStringProvider>(_ =>
      new ConnectionStringProvider(connectionString ?? throw new InvalidOperationException()));
    services.AddTransient<IGuildConfigurationRepository, GuildConfigurationRepository>();
  }

  public static void UseDataAccessLayer(this IApplicationBuilder app)
  {
    using var scope = app.ApplicationServices.CreateScope();
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
  }
}