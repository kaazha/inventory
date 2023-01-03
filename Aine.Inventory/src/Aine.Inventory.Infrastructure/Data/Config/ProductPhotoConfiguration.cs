using Aine.Inventory.Core.ProductAggregate;
using Aine.Inventory.Core.ProductPhotoAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aine.Inventory.Infrastructure.Data.Config;

public class ProductPhotoConfiguration : EntityConfigurationBase<ProductPhoto>, IEntityTypeConfiguration<ProductPhoto>
{
  public void Configure(EntityTypeBuilder<ProductPhoto> builder)
  {
    builder.ToTable("product_photo");
    builder.Property(t => t.Id).HasColumnName("id");
    builder.HasKey(p => p.Id);
    //builder.HasOne(typeof(Product), "Id")
    //      .WithOne()
    //      .HasForeignKey("ProductPhoto", "ProductId")
    //      .OnDelete(DeleteBehavior.Cascade);

    builder.Property(p => p.ProductId).HasColumnName("product_id");
    builder.Property(p => p.ThumbNailPhotoFileName).HasColumnName("thumbnail_photo_filename");
    builder.Property(p => p.LargePhotoFileName).HasColumnName("large_photo_filename");
  }
}

