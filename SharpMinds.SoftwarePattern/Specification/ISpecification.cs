using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace SharpMinds.SoftwarePattern.Specification;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    
    Expression<Func<T, object>> OrderBy { get; }
    Expression<Func<T, object>> OrderByDescending { get; }

    public Func<IQueryable<T>, IIncludableQueryable<T, object>>[] GetIncludes();
}