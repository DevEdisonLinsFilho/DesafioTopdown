using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoTopdown.Domain.Entities;

namespace ProjetoTopdown.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));
        
        builder.ToTable("customers");
        
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(c => c.Email)
            .IsUnique();

        builder.Property(c => c.Document)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.HasData(SeedData.GenerateCustomers(20));
    }
}