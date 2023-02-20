using System;
using Aine.Inventory.Core.ProductPriceAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aine.Inventory.Infrastructure.Data.Config;

public class ProductPriceConfiguration : EntityConfigurationBase<ProductPrice>, IEntityTypeConfiguration<ProductPrice>
{
  public void Configure(EntityTypeBuilder<ProductPrice> builder)
  {
    builder.ToTable("product_price");
    builder.HasKey(p => p.Id);
    ConfigureColumnNames(builder);
  }
}