using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace SharpMinds.SoftwarePattern.Specification;

public class BaseSpecification<T> : ISpecification<T>
{
    public Expression<Func<T, bool>> Criteria { get; protected set; }

    public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> IncludeExpressions { get; }
        = new List<Func<IQueryable<T>, IIncludableQueryable<T, object>>>();

    public Expression<Func<T, object>> OrderBy { get; set; }
    public Expression<Func<T, object>> OrderByDescending { get; set; }
    public int Take { get; set; }
    public int Skip { get; set; }
    public bool IsPagingEnabled { get; set; }

    protected BaseSpecification()
    {
    }

    protected BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public static BaseSpecification<T> Create(Expression<Func<T, bool>> criteria)
    {
        return new BaseSpecification<T>(criteria);
    }

    public BaseSpecification<T> AddInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> includeExpression)
    {
        IncludeExpressions.Add(includeExpression);
        return this;
    }

    public BaseSpecification<T> ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
        return this;
    }

    public BaseSpecification<T> ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
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