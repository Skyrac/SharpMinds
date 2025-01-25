using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SharpMinds.SoftwarePattern.Repository.Domain;

public class Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}

public class OrderItem
{
    public int Id { get; set; }
    public string SKU { get; set; }
    public Product Product { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        // Primärschlüssel
        builder.HasKey(o => o.Id);

        // Beispiel: Property-Eigenschaften
        builder.Property(o => o.OrderNumber)
            .IsRequired()               // Pflichtfeld
            .HasMaxLength(50);         // maximale Länge

        // Beziehung: 1 Order -> n OrderItems
        // Da OrderItem eine Navigation "Product" hat, wird hier 
        // nur festgelegt, dass OrderItems zur Order gehören.
        builder.HasMany(o => o.OrderItems)
            .WithOne()              // Falls du in OrderItem keine explizite `Order`-Eigenschaft hast
            .HasForeignKey("OrderId") // Falls du das FK-Feld "OrderId" in der DB haben willst
            .OnDelete(DeleteBehavior.Cascade); 
        // Cascade: Wenn eine Order gelöscht wird, werden alle zugehörigen Items gelöscht
    }
}

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.SKU)
            .IsRequired()
            .HasMaxLength(50);

        // Beziehung: 1 OrderItem -> 1 Product
        // Falls du in Product nicht umgekehrt auf OrderItem verweist,
        // dann .WithMany() (oder .WithOne(null) / .WithOne() je nach Modell).
        builder.HasOne(oi => oi.Product)
            .WithMany()         // oder .WithOne() falls Product -> Single OrderItem
            .HasForeignKey("ProductId")
            .OnDelete(DeleteBehavior.Restrict); 
        // Restrict: Verhindert, dass ein Product gelöscht wird, 
        // wenn noch OrderItems darauf referenzieren.
    }
}

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}