using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .ToTable("products")
            .HasKey(p => p.Id);

        builder
            .Property(p => p.Name)
            .IsRequired();

        builder
            .Property(p => p.Price)
            .IsRequired();

        builder
            .Property(p => p.CustomerId)
            .IsRequired();
    }
}