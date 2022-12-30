using Aine.Inventory.Core.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aine.Inventory.Infrastructure.Data.Config;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
  public void Configure(EntityTypeBuilder<Product> builder)
  {
    builder.ToTable("products");
    builder.Property(t => t.Id).HasColumnName("id");
    builder.HasKey(p => p.Id);
    builder.Property(t => t.Name)
      .HasColumnName("name")
      .HasMaxLength(128)
        .IsRequired();
    builder.Property(t => t.Description)
        .HasColumnName("description")
        .HasMaxLength(250)
        .IsRequired(false);
  }
}

