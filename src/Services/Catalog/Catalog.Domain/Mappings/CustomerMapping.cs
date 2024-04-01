using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class CustomerMapping : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder
            .ToTable("customers")
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Name)
            .IsRequired();

        builder
            .Property(c => c.Email)
            .IsRequired();
    }
}