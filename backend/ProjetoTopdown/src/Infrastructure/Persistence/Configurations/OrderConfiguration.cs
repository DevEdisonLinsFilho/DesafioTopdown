using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoTopdown.Domain.Entities;

namespace ProjetoTopdown.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        builder.ToTable("orders");
        builder.HasKey(o => o.Id);

        builder.Property(o => o.IdempotencyKey)
            .IsRequired();

        builder.HasIndex(o => o.IdempotencyKey)
            .IsUnique();

        builder.HasOne(o => o.Customer)
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}