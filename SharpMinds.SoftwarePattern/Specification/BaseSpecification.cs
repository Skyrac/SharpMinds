using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace SharpMinds.SoftwarePattern.Specification;

public class BaseSpecification<T> : ISpecification<T>
{
    public Expression<Func<T, bool>> Criteria { get; protected set; }

    List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> IncludeExpressions { get; }
        = new ();

    public Expression<Func<T, object>> OrderBy { get; set; }
    public Expression<Func<T, object>> OrderByDescending { get; set; }

    protected BaseSpecification()
    {
    }

    public Func<IQueryable<T>, IIncludableQueryable<T, object>>[] GetIncludes()
    {
        return IncludeExpressions.ToArray();
    }

    public static BaseSpecification<T> Create()
    {
        return new BaseSpecification<T>();
    }

    protected BaseSpecification<T> AddInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> includeExpression)
    {
        IncludeExpressions.Add(includeExpression);
        return this;
    }
    
    public BaseSpecification<T> ApplyCriteria(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
        return this;
    }
    
    public BaseSpecification<T> ApplyOrder(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
        return this;
    }

    public BaseSpecification<T> ApplyOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
        return this;
    }
}