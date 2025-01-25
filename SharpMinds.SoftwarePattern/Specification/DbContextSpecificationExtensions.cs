using Microsoft.EntityFrameworkCore;

namespace SharpMinds.SoftwarePattern.Specification;

public static class DbContextSpecificationExtensions
{
    public static Task<List<T>> FromSpecificationAsync<T>(
        this DbContext context, 
        ISpecification<T> specification) 
        where T : class
    {
        var query = context.Set<T>().AsQueryable();
        
        query = SpecificationEvaluator<T>.GetQuery(query, specification);

        return query.AsNoTracking().ToListAsync();
    }
}