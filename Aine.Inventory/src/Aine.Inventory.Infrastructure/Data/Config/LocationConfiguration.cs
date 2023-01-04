using Aine.Inventory.Core.CategoryAggregate;
using Aine.Inventory.Core.ProductInventoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aine.Inventory.Infrastructure.Data.Config;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
  public void Configure(EntityTypeBuilder<Location> builder)
  {
    builder.ToTable("location");
    builder.Property(t => t.Id).HasColumnName("id");
    builder.HasIndex(p => p.Name).IsUnique();
    builder.HasKey(p => p.Id);
    builder.Property(t => t.Name)
      .HasColumnName("name")
      .HasMaxLength(ProductCategory.MAX_NAME_LENGTH)
        .IsRequired();    
  }
}

