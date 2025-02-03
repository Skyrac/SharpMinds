using Microsoft.EntityFrameworkCore;
using SharpMinds.SoftwarePattern.Specification;

namespace SharpMinds.SoftwarePattern.Repository;

public interface IGenericRepository<TEntity> 
    where TEntity : class
{
    ValueTask<TEntity?> GetByIdAsync(object? id);
    Task<List<TEntity>> ListAllAsync();
    Task<List<TEntity>> QueryBySpecification(ISpecification<TEntity> spec);
    Task<Page<TEntity>> QueryBySpecificationPaged(ISpecification<TEntity> spec, int page = 1, int pageSize = 50);

    /// <inheritdoc cref="DbContext.Add(TEntity)"/>
    Task Add(params TEntity[] entity);
    
    /// <inheritdoc cref="DbContext.Update(TEntity)"/>
    void Update(params TEntity[] entity);
    void Remove(params TEntity[] entity);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}