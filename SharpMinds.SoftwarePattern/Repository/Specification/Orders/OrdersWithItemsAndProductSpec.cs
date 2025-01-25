using SharpMinds.SoftwarePattern.Repository.Domain;
using SharpMinds.SoftwarePattern.Repository.Specification.Orders.Navigations;
using SharpMinds.SoftwarePattern.Specification;

namespace SharpMinds.SoftwarePattern.Repository.Specification.Orders;

public class OrdersWithItemsAndProductSpec : BaseSpecification<Order>
{
    public OrdersWithItemsAndProductSpec()
    {
        Criteria = o => o.Id > 0;
        
        AddInclude(OrderNavigations.WithItemsAndProduct);
    }
}