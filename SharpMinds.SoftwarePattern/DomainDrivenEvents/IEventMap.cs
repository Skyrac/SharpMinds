namespace SharpMinds.SoftwarePattern.DomainDrivenEvents;

public interface IEventMap
{
    public IDomainEvent? Created { get; }
    public IDomainEvent? Deleted { get; }
    public IDomainEvent? Updated { get; }
}