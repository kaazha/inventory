using Aine.Inventory.Core.CategoryAggregate;
using Aine.Inventory.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aine.Inventory.Infrastructure.Data.Config;

public class AuthTokenConfiguration : IEntityTypeConfiguration<AuthToken>
{
  public void Configure(EntityTypeBuilder<AuthToken> builder)
  {
    builder.ToTable("auth_tokens");
    builder.Property(t => t.UserId).HasColumnName("user_id").HasMaxLength(128);
    builder.Property(t => t.ExpiryDate).HasColumnName("expiry_date");
    builder.Property(t => t.Token).HasColumnName("token").HasMaxLength(4000);
    builder.HasKey(p => p.UserId);
  }
}