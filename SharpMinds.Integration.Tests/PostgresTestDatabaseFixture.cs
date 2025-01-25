using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharpMinds.SoftwarePattern.Repository;
using Testcontainers.PostgreSql;

namespace SharpMinds.Integration.Tests;

public class PostgresTestDatabaseFixture : IAsyncLifetime
{
    public ServiceProvider ServiceProvider { get; private set; }
    private readonly PostgreSqlContainer _container;

    public PostgresTestDatabaseFixture()
    {
        // Testcontainer für Postgres definieren
        _container = new PostgreSqlBuilder()
            .WithDatabase("testdb")
            .WithUsername("postgres")
            .WithPassword("password")
            .Build();
    }

    public async Task InitializeAsync()
    {
        // 1) Container starten etc...
        await _container.StartAsync();

        var serviceCollection = new ServiceCollection();

        // 2) DbContext registrieren
        serviceCollection.AddDbContext<RepositoryDbContext>(options =>
            options.UseNpgsql(_container.GetConnectionString()));

        // 3) Repository oder andere Services registrieren
        serviceCollection.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

        // 4) ServiceProvider aufbauen
        ServiceProvider = serviceCollection.BuildServiceProvider();

        // 5) Migrations/EnsureCreated
        var context = ServiceProvider.GetRequiredService<RepositoryDbContext>();
        await context.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        // Container stoppen
        await _container.DisposeAsync();
    }
}