namespace SharpMinds.SoftwarePattern.Specification;

public static class SpecificationEvaluator<T> where T : class
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
    {
        var query = inputQuery;

        // WHERE
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }

        // MULTI-LEVEL Includes via Func<IQueryable<T>, IIncludableQueryable<T, object>>
        // Achte darauf, dass du den Typ in ISpecification<T> ergänzt (IncludeExpressions etc.)
        foreach (var includeExpression in spec.IncludeExpressions)
        {
            query = includeExpression(query);
        }

        // ORDER BY
        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        else if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        // PAGING
        if (spec.IsPagingEnabled)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }

        return query;
    }
}