using Microsoft.EntityFrameworkCore.Query;

namespace SharpMinds.SoftwarePattern.Specification;

public class Includable<T> : IIncludable<T>
{
    public Func<IQueryable<T>, IIncludableQueryable<T, object>> Includes { get; }
}


public interface IIncludable<T>
{
    public Func<IQueryable<T>, IIncludableQueryable<T, object>> Includes { get; }
}