using Microsoft.EntityFrameworkCore;
using SharpMinds.SoftwarePattern.Specification;

namespace SharpMinds.SoftwarePattern.Repository;

public class GenericRepository<TEntity, TDbContext>(TDbContext context) : IGenericRepository<TEntity>
    where TEntity : class
    where TDbContext : DbContext
{
    public async Task<TEntity?> GetByIdAsync(object? id)
    {
        return await context.Set<TEntity>().FindAsync(id);
    }

    public async Task<List<TEntity>> ListAllAsync()
    {
        return await context.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task<List<TEntity>> BySpecification(ISpecification<TEntity> spec)
    {
        var query = ApplySpecification(spec);
        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<Page<TEntity>> BySpecificationPaged(ISpecification<TEntity> spec, int page = 1, int pageSize = 50)
    {
        var query = ApplySpecification(spec);
        var count = await query.CountAsync();
        var results = await query
            .AsNoTracking()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        var pages = count / (pageSize * page);
        return new Page<TEntity>(page, Math.Min(pages, page + 1),pages,pageSize,count, results);
    }

    public TEntity Add(TEntity entity)
    {
        context.Add(entity);
        return entity;
    }
    
    public void Update(TEntity entity)
    {
        context.Update(entity);
    }

    public void Remove(TEntity entity)
    {
        context.Remove(entity);
    }
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
    {
        return SpecificationEvaluator<TEntity>.GetQuery(context.Set<TEntity>().AsQueryable(), spec);
    }
}