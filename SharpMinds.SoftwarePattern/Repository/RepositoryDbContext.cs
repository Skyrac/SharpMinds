using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace SharpMinds.SoftwarePattern.Repository;

public class RepositoryDbContext : DbContext
{
    public RepositoryDbContext(DbContextOptions<RepositoryDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}