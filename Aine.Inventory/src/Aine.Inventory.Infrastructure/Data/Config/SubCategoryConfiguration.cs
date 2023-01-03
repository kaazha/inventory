using Aine.Inventory.Core.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aine.Inventory.Infrastructure.Data.Config;

public class SubCategoryConfiguration : IEntityTypeConfiguration<ProductSubCategory>
{
  public void Configure(EntityTypeBuilder<ProductSubCategory> builder)
  {
    builder.ToTable("product_subcategory");
    builder.HasKey(p => p.Id);
    builder.Property(t => t.Id).HasColumnName("id");
    builder.Property(t => t.CategoryId).HasColumnName("category_id");
    builder.HasIndex(p => p.Name).IsUnique();
    
    builder.Property(t => t.Name)
      .HasColumnName("name")
      .HasMaxLength(ProductCategory.MAX_NAME_LENGTH)
        .IsRequired();

    builder.Property(t => t.Description)
        .HasColumnName("description")
        .HasMaxLength(250)
        .IsRequired(false);

    builder.HasOne(p => p.Category).WithMany(p => p.SubCategories).OnDelete(DeleteBehavior.Cascade);
  }
}

