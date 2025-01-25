using Microsoft.EntityFrameworkCore;
using SharpMinds.SoftwarePattern.Specification;

namespace SharpMinds.SoftwarePattern.Repository;

public interface IGenericRepository<TEntity, TDbContext> 
    where TEntity : class 
    where TDbContext : DbContext
{
    Task<TEntity?> GetByIdAsync(object? id);
    Task<List<TEntity>> ListAllAsync();
    Task<List<TEntity>> ListAsync(ISpecification<TEntity> spec);

    /// <inheritdoc cref="DbContext.Add(TEntity)"/>
    TEntity Add(TEntity entity);
    
    /// <inheritdoc cref="DbContext.Update(TEntity)"/>
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}