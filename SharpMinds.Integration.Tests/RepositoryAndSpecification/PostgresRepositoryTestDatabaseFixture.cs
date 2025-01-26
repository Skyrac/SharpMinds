using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharpMinds.SoftwarePattern.Repository;
using Testcontainers.PostgreSql;

namespace SharpMinds.Integration.Tests.RepositoryAndSpecification;

public class PostgresRepositoryTestDatabaseFixture : IAsyncLifetime
{
    public ServiceProvider ServiceProvider { get; private set; }
    private readonly PostgreSqlContainer _container;

    public PostgresRepositoryTestDatabaseFixture()
    {
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

        serviceCollection.AddDbContext<RepositoryDbContext>(options =>
            options.UseNpgsql(_container.GetConnectionString()));

        serviceCollection.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

        ServiceProvider = serviceCollection.BuildServiceProvider();

        var context = ServiceProvider.GetRequiredService<RepositoryDbContext>();
        await context.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await _container.DisposeAsync();
    }
}