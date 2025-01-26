using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SharpMinds.SoftwarePattern.DomainDrivenEvents.Domain;

public class DomainOrder : DomainEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsFinished { get; set; } = false;
    public override IEventMap? DomainEvents { get; } = new OrderEventMap();

    public class OrderEventMap : IEventMap
    {
        public IDomainEvent? Created { get; } = new DomainOrderCreated();
        public IDomainEvent? Deleted { get; }
        public IDomainEvent? Updated { get; }
    }

    public class DomainOrderProfile : Profile
    {
        public DomainOrderProfile()
        {
            CreateMap<DomainOrder, DomainOrderCreated>();
        }
    }

    public class DomainOrderDbConfiguration : IEntityTypeConfiguration<DomainOrder>
    {
        public void Configure(EntityTypeBuilder<DomainOrder> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}

public class DomainOrderCreated : IDomainEvent
{
    public int Id { get; set; }
    public string Name { get; set; }
}