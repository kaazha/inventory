using Aine.Inventory.Core.ProductModelAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aine.Inventory.Infrastructure.Data.Config;

public class ProductModelConfiguration : IEntityTypeConfiguration<ProductModel>
{
  public void Configure(EntityTypeBuilder<ProductModel> builder)
  {
    builder.ToTable("product_model");

    builder.Property(t => t.Id).HasColumnName("id");

    builder.HasIndex(p => p.Name).IsUnique();

    builder.HasKey(p => p.Id);

    builder.Property(t => t.Name)
      .HasColumnName("name")
      .HasMaxLength(ProductModel.MAX_NAME_LENGTH)
        .IsRequired();

    builder.Property(t => t.Description)
        .HasColumnName("description")
        .HasMaxLength(500)
        .IsRequired();
  }
}

