namespace SharpMinds.SoftwarePattern.DomainDrivenEvents;

public abstract class DomainEntity
{
    public abstract IEventMap? DomainEvents { get; }
    public List<IDomainEvent> CurrentEvents { get; } = new();
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        CurrentEvents.Add(domainEvent);
    }
}