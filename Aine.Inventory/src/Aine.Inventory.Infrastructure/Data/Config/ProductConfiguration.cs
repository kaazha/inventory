using Aine.Inventory.Core.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aine.Inventory.Infrastructure.Data.Config;

public class ProductConfiguration : EntityConfigurationBase<Product>, IEntityTypeConfiguration<Product>
{
  public void Configure(EntityTypeBuilder<Product> builder)
  {
    builder.ToTable("product");

    builder.Property(t => t.Id)
      .HasColumnName("id");

    builder.Property(t => t.ProductNumber)
      .HasColumnName("product_number")
      .IsRequired()
      .HasMaxLength(128);

    builder.HasIndex(p => p.ProductNumber).IsUnique();
    builder.HasKey(p => p.Id);

    builder.Property(t => t.Name)
      .HasColumnName("name")
      .HasMaxLength(128)
      .IsRequired();

    builder.Property(t => t.Description)
      .HasColumnName("description")
      .HasMaxLength(500)
      .IsRequired(false);

    builder.HasOne(p => p.SubCategory).WithMany().OnDelete(DeleteBehavior.NoAction);
    builder.HasOne(p => p.Model).WithMany().OnDelete(DeleteBehavior.NoAction);

    ConfigureColumnNames(builder);
  }
}

