using Microsoft.EntityFrameworkCore;
using SharpMinds.SoftwarePattern.Specification;

namespace SharpMinds.SoftwarePattern.Repository;

public class GenericRepository<TEntity, TDbContext> : IGenericRepository<TEntity, TDbContext> 
    where TEntity : class 
    where TDbContext : DbContext
{
    private readonly RepositoryDbContext _context;

    public GenericRepository(RepositoryDbContext context)
    {
        _context = context;
    }

    public async Task<TEntity?> GetByIdAsync(object? id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<List<TEntity>> ListAllAsync()
    {
        return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    public async Task<List<TEntity>> ListAsync(ISpecification<TEntity> spec)
    {
        var query = ApplySpecification(spec);
        return await query.AsNoTracking().ToListAsync();
    }

    public TEntity Add(TEntity entity)
    {
        _context.Add(entity);
        return entity;
    }
    
    public void Update(TEntity entity)
    {
        _context.Update(entity);
    }

    public void Remove(TEntity entity)
    {
        _context.Remove(entity);
    }
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
    {
        return SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), spec);
    }
}