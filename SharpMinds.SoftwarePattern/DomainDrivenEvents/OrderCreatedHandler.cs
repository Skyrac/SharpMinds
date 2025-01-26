using MediatR;
using Microsoft.EntityFrameworkCore;
using SharpMinds.SoftwarePattern.DomainDrivenEvents.Domain;

namespace SharpMinds.SoftwarePattern.DomainDrivenEvents;

public class OrderCreatedHandler(DomainDbContext context) : INotificationHandler<DomainOrderCreated>
{
    public async Task Handle(DomainOrderCreated notification, CancellationToken cancellationToken)
    {
        var entity = await context.Set<DomainOrder>().FirstAsync(x => x.Id == notification.Id);
        entity.IsFinished = true;
        await context.SaveChangesAsync(cancellationToken);
    }
}