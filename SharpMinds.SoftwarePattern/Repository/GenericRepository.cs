using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SharpMinds.SoftwarePattern.Specification;

namespace SharpMinds.SoftwarePattern.Repository;

public class GenericRepository<TEntity, TDbContext>(TDbContext context) : IGenericRepository<TEntity>
    where TEntity : class
    where TDbContext : DbContext
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    public ValueTask<TEntity?> GetByIdAsync(object? id)
    {
        return _dbSet.FindAsync(id);
    }

    public async Task<List<TEntity>> ListAllAsync()
    {
        return await _dbSet
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<List<TEntity>> QueryBySpecification(ISpecification<TEntity> spec)
    {
        var query = ApplySpecification(spec);
        return await query.AsNoTracking().ToListAsync();
    }

    public Task<Page<TEntity>> QueryBySpecificationPaged(ISpecification<TEntity> spec, int page = 1, int pageSize = 50)
    {
        var query = ApplySpecification(spec);
        return GetPaged(query, page, pageSize);
    }

    public Task Add(params TEntity[] entities)
    {
        return _dbSet.AddRangeAsync(entities);
    }
    
    public void Update(params TEntity[] entity)
    {
        _dbSet.UpdateRange(entity);
    }

    public void Remove(params TEntity[] entity)
    {
        _dbSet.RemoveRange(entity);
    }
    
    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
    
    private async Task<Page<TEntity>> GetPaged(IQueryable<TEntity> query, int page, int pageSize)
    {
        var count = await query.CountAsync();
        var results = await query
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var pages = count / (pageSize * page);
        return new Page<TEntity>(page, Math.Min(pages, page + 1),pages,pageSize,count, results);
    }

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
    {
        return SpecificationEvaluator<TEntity>.GetQuery(_dbSet.AsQueryable(), spec);
    }
}