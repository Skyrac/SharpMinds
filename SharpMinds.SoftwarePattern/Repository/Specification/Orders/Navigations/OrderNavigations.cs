using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SharpMinds.SoftwarePattern.Repository.Domain;

namespace SharpMinds.SoftwarePattern.Repository.Specification.Orders.Navigations;

public static class OrderNavigations
{
    public static Func<IQueryable<Order>, IIncludableQueryable<Order, object>> WithItemsAndProduct 
        = query => query
            .Include(o => o.OrderItems)
            .ThenInclude(i => i.Product);

    // Du könntest weitere statische Funktionen bereitstellen, z. B.
    public static Func<IQueryable<Order>, IIncludableQueryable<Order, object>> WithItems
        = query => query
            .Include(o => o.OrderItems);
}