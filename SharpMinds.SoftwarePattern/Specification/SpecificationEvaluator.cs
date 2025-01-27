namespace SharpMinds.SoftwarePattern.Specification;

public static class SpecificationEvaluator<T> where T : class
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
    {
        var query = inputQuery;
        
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }
        
        foreach (var includeExpression in spec.GetIncludes())
        {
            query = includeExpression(query);
        }

        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        else if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        return query;
    }
}