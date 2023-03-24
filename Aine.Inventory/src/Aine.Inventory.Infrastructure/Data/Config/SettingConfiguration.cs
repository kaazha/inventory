using Aine.Inventory.Core.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aine.Inventory.Infrastructure.Data.Config;

public class SettingConfiguration : IEntityTypeConfiguration<Setting>
{
  public void Configure(EntityTypeBuilder<Setting> builder)
  {
    builder.ToTable("settings");
    builder.Property(t => t.Name).HasColumnName("setting_name").HasMaxLength(128);
    builder.Property(t => t.Value).HasColumnName("setting_value").HasMaxLength(255);
    builder.HasKey(p => p.Name);

    builder.Property(t => t.Description)
        .HasColumnName("description")
        .HasMaxLength(250)
        .IsRequired(false);
  }
}

public class Setting
{
  public string Name { get; private set; } = default!;
  public string Description { get; private set; } = default!;
  public string Value { get; private set; } = default!;
}