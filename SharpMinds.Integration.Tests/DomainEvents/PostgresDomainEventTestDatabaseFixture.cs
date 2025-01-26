using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharpMinds.SoftwarePattern.DomainDrivenEvents;
using SharpMinds.SoftwarePattern.Repository;
using Testcontainers.PostgreSql;

namespace SharpMinds.Integration.Tests.DomainEvents;

public class PostgresDomainEventTestDatabaseFixture : IAsyncLifetime
{
    public ServiceProvider ServiceProvider { get; private set; }
    private readonly PostgreSqlContainer _container;

    public PostgresDomainEventTestDatabaseFixture()
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
        await _container.StartAsync();

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(DomainDbContext).Assembly));
        serviceCollection.AddAutoMapper(x => x.AddMaps(typeof(DomainDbContext).Assembly));
        serviceCollection.AddDbContext<DomainDbContext>(options =>
            options.UseNpgsql(_container.GetConnectionString()));

        ServiceProvider = serviceCollection.BuildServiceProvider();

        var context = ServiceProvider.GetRequiredService<DomainDbContext>();
        await context.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        // Container stoppen
        await _container.DisposeAsync();
    }
}