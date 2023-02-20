using Aine.Inventory.Core.ProductInventoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aine.Inventory.Infrastructure.Data.Config;

public class ProductInventoryConfiguration : EntityConfigurationBase<ProductInventory>, IEntityTypeConfiguration<ProductInventory>
{
  public void Configure(EntityTypeBuilder<ProductInventory> builder)
  {
    builder.ToTable("product_inventory");
    builder.HasKey(p => p.Id);
    ConfigureColumnNames(builder);
  }
}

