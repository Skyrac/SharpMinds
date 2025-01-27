using Microsoft.EntityFrameworkCore;
using SharpMinds.SoftwarePattern.Specification;

namespace SharpMinds.SoftwarePattern.Repository;

public interface IGenericRepository<TEntity> 
    where TEntity : class 
{
    Task<List<TEntity>> ListAllAsync();
    Task<List<TEntity>> BySpecification(ISpecification<TEntity> spec);
    Task<Page<TEntity>> BySpecificationPaged(ISpecification<TEntity> spec, int page = 1, int pageSize = 50);

    /// <inheritdoc cref="DbContext.Add(TEntity)"/>
    TEntity Add(TEntity entity);
    
    /// <inheritdoc cref="DbContext.Update(TEntity)"/>
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}