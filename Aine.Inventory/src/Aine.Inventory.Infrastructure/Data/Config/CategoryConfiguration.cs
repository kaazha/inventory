using Aine.Inventory.Core.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aine.Inventory.Infrastructure.Data.Config;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
  public void Configure(EntityTypeBuilder<Category> builder)
  {
    builder.ToTable("categories");

    builder.Property(t => t.Id).HasColumnName("id");

    builder.HasKey(p => p.Id);

    builder.Property(t => t.Name)
      .HasColumnName("name")
      .HasMaxLength(50)
        .IsRequired();

    builder.Property(t => t.Description)
        .HasColumnName("description")
        .HasMaxLength(250)
        .IsRequired(false);
  }
}

