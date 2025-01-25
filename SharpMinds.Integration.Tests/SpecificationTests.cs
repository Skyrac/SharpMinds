using Microsoft.Extensions.DependencyInjection;
using SharpMinds.SoftwarePattern.Repository;
using SharpMinds.SoftwarePattern.Repository.Domain;
using SharpMinds.SoftwarePattern.Repository.Specification.Orders;
using SharpMinds.SoftwarePattern.Specification;

namespace SharpMinds.Integration.Tests;

[Collection(nameof(PostgresTestCollection))]
public class SpecificationTests(PostgresTestDatabaseFixture fixture)
{
    [Fact]
    public async Task Specification_With_Repository()
    {
        var repo = fixture.ServiceProvider.GetRequiredService<IGenericRepository<Order, RepositoryDbContext>>();
        repo.Add(new Order()
        {
            Id = 1,
            OrderNumber = "Test",
            OrderItems = new List<OrderItem>()
            {
                new OrderItem()
                {
                    Id = 1,
                    SKU = "TSKU",
                    Product = new Product()
                    {
                        Id = 1,
                        Name = "TestProduct"
                    }
                }
            }
        });
        await repo.SaveChangesAsync();
        var result = await repo.ListAsync(new OrdersWithItemsAndProductSpec());
        
        Assert.NotEmpty(result);
    }
    
    [Fact]
    public async Task Specification_Standalone()
    {
        var db = fixture.ServiceProvider.GetRequiredService<RepositoryDbContext>();
        db.Add(new Order()
        {
            Id = 1,
            OrderNumber = "Test",
            OrderItems = new List<OrderItem>()
            {
                new OrderItem()
                {
                    Id = 1,
                    SKU = "TSKU",
                    Product = new Product()
                    {
                        Id = 1,
                        Name = "TestProduct"
                    }
                }
            }
        });
        await db.SaveChangesAsync();
        var result = await db.FromSpecificationAsync(new OrdersWithItemsAndProductSpec());
        
        Assert.NotEmpty(result);
    }
    
    [Fact]
    public async Task SimpleWhere_Specification_Standalone()
    {
        var db = fixture.ServiceProvider.GetRequiredService<RepositoryDbContext>();
        db.AddRange([
            new Order()
            {
                Id = 1,
                OrderNumber = "Test",
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        Id = 1,
                        SKU = "TSKU",
                        Product = new Product()
                        {
                            Id = 1,
                            Name = "TestProduct"
                        }
                    }
                }
            },
            new Order()
            {
                Id = 2,
                OrderNumber = "Test Two",
                OrderItems = new List<OrderItem>()
                {
                    new OrderItem()
                    {
                        Id = 2,
                        SKU = "TSKUT",
                        Product = new Product()
                        {
                            Id = 2,
                            Name = "TestProduct 2"
                        }
                    }
                }
            }
        ]);
        await db.SaveChangesAsync();
        var result = await db.FromSpecificationAsync(
            BaseSpecification<Order>.Create(x => x.OrderNumber == "Test")
                .ApplyPaging(0, 1)
        );
        
        Assert.NotEmpty(result);
        Assert.Single(result);
    }
}