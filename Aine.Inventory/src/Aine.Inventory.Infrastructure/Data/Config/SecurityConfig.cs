using Aine.Inventory.Core.CategoryAggregate;
using Aine.Inventory.Core.Helpers;
using Aine.Inventory.SharedKernel.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aine.Inventory.Infrastructure.Data.Config;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
  public void Configure(EntityTypeBuilder<Role> builder)
  {
    builder.ToTable("roles");
    builder.Property(t => t.RoleName).HasColumnName("role_name").HasMaxLength(128);
    builder.Property(t => t.Description).HasColumnName("role_description").HasMaxLength(255);
    builder.HasKey(p => p.Id);
    builder.HasIndex(p => p.RoleName).IsUnique();
  }
}

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
  public void Configure(EntityTypeBuilder<Permission> builder)
  {
    builder.ToTable("permissions");
    builder.Property(t => t.PermissionTitle).HasColumnName("permission_title").HasMaxLength(128);
    builder.Property(t => t.Description).HasColumnName("permission_description").HasMaxLength(255);
    builder.HasKey(p => p.Id);
    builder.HasIndex(p => p.PermissionTitle).IsUnique();
  }
}

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
  public void Configure(EntityTypeBuilder<UserRole> builder)
  {
    builder.ToTable("user_roles");
    builder.Property(t => t.UserId).HasColumnName("user_id");
    builder.Property(t => t.RoleId).HasColumnName("role_id");
    builder.HasKey(p => new { p.UserId , p.RoleId});

    builder.HasOne(p => p.Role); 
  }
}

public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
{
  public void Configure(EntityTypeBuilder<UserPermission> builder)
  {
    builder.ToTable("user_permissions");
    builder.Property(t => t.UserId).HasColumnName("user_id");
    builder.Property(t => t.PermissionId).HasColumnName("permission_id");
    builder.Property(t => t.PermissionFlag).HasColumnName("permission_flag");
    builder.HasKey(p => new { p.UserId, p.PermissionId });

    builder.HasOne(p => p.Permission);
  }
}

public class UserConfiguration : EntityConfigurationBase<User>, IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("users");
    ConfigureColumnNames(builder);
    builder.Property(t => t.Id).HasColumnName("user_id");
    builder.HasKey(p => p.Id);
    builder.HasIndex(p => p.UserName).IsUnique();

    builder.HasMany(p => p.Permissions);
    builder.HasMany(p => p.UserRoles);
  }
}