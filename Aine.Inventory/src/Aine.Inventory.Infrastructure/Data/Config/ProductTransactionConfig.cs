using System;
using Aine.Inventory.Core.TransactionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aine.Inventory.Infrastructure.Data.Config;

public class ProductTransactionConfig : EntityConfigurationBase<ProductTransaction>, IEntityTypeConfiguration<ProductTransaction>
{
  public void Configure(EntityTypeBuilder<ProductTransaction> builder)
  {
    builder.ToTable("product_transaction");
    builder.HasKey(p => p.Id);
    ConfigureColumnNames(builder);
  }
}