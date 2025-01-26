using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharpMinds.SoftwarePattern.DomainDrivenEvents;
using SharpMinds.SoftwarePattern.DomainDrivenEvents.Domain;

namespace SharpMinds.Integration.Tests.DomainEvents;

[Collection(nameof(PostgresDomainEventTestCollection))]
public class DomainEventTest(PostgresDomainEventTestDatabaseFixture fixture)
{
    [Fact]
    public async Task Test()
    {
        var db = fixture.ServiceProvider.GetRequiredService<DomainDbContext>();
        db.Add(new DomainOrder()
        {
            Id = 1,
            Name = "Test"
        });
        await db.SaveChangesAsync();
        await Task.Delay(200);
        var order = await db.Set<DomainOrder>().FirstAsync(x => x.Id == 1);
        Assert.True(order.IsFinished);
    }
}