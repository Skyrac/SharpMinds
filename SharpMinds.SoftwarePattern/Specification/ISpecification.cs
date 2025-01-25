using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace SharpMinds.SoftwarePattern.Specification;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }

    public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> IncludeExpressions { get; }
    
    Expression<Func<T, object>> OrderBy { get; }
    Expression<Func<T, object>> OrderByDescending { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
}