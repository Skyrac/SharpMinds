using MediatR;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;

namespace SharpMinds.SoftwarePattern.DomainDrivenEvents;

/// <summary>
/// The SaveChange behavior to send/store domain events (in case of outbox pattern)
/// can also be deferred to Repository Pattern
/// </summary>
public class DomainDbContext : DbContext
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    public DomainDbContext(DbContextOptions<DomainDbContext> options, 
        IMapper mapper,
        IMediator mediator)
        : base(options)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public override int SaveChanges()
    {
        var events = StoreEntityEvents();
        var result = base.SaveChanges();
        DispatchEvents(events);
        return result;
    }

    private void DispatchEvents(List<IDomainEvent> events)
    {
        foreach (var ev in events)
        {
            _mediator.Publish(ev);
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var events = StoreEntityEvents();
        var result = base.SaveChangesAsync(cancellationToken);
        DispatchEvents(events);
        return result;
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        var events = StoreEntityEvents();
        var result = base.SaveChanges(acceptAllChangesOnSuccess);
        DispatchEvents(events);
        return result;
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        var events = StoreEntityEvents();
        var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        DispatchEvents(events);
        return result;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DomainDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    private List<IDomainEvent> StoreEntityEvents()
    {
        var changedEntries = ChangeTracker.Entries()
            .Where(e => e.State != EntityState.Unchanged)
            .ToList();
        var events = new List<IDomainEvent>();
        foreach (var entry in changedEntries)
        {
            if (entry.Entity is DomainEntity domainEntity)
            {
                var eventMap = domainEntity.DomainEvents;
                if (eventMap == null) continue;
                switch (entry.State)
                {
                    case EntityState.Added:
                    if (eventMap.Created != null)
                    {
                        var ev = _mapper.Map(domainEntity, eventMap.Created);
                        events.Add(ev);
                    }
                    break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                        break;
                    case EntityState.Modified:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        return events;
    }
}