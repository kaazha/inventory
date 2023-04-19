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
    builder.ToTable("iam_roles");
    builder.Property(t => t.RoleName).HasColumnName("role_name").HasMaxLength(128);
    builder.Property(t => t.Description).HasColumnName("role_description").HasMaxLength(255);
    builder.Property(t => t.IsAdminRole).HasColumnName("is_admin_role");
    builder.HasKey(p => p.Id);
    builder.HasIndex(p => p.RoleName).IsUnique();
  }
}

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
  public void Configure(EntityTypeBuilder<Permission> builder)
  {
    builder.ToTable("iam_permissions");
    builder.Property(t => t.PermissionTitle).HasColumnName("permission_title").HasMaxLength(128);
    builder.Property(t => t.Description).HasColumnName("permission_description").HasMaxLength(255);
    builder.Property(t => t.PermissionType).HasColumnName("permission_type").HasConversion<string>().HasMaxLength(50);
    builder.HasKey(p => p.Id);
    builder.HasIndex(p => p.PermissionTitle).IsUnique();
  }
}

public class UserConfiguration : EntityConfigurationBase<User>, IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("iam_users");
    ConfigureColumnNames(builder);
    builder.Property(t => t.Id).HasColumnName("user_id");
    builder.Property(t => t.LastLogIn).HasColumnName("last_login");
    builder.HasKey(p => p.Id);
    builder.HasIndex(p => p.UserName).IsUnique();

    builder.HasMany(p => p.Permissions);
    builder.HasMany(p => p.Roles);
  }
}

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
  public void Configure(EntityTypeBuilder<UserRole> builder)
  {
    builder.ToTable("iam_user_roles");
    builder.Property(t => t.UserId).HasColumnName("user_id");
    builder.Property(t => t.RoleId).HasColumnName("role_id");
    builder.HasKey(p => new { p.UserId , p.RoleId});
    builder.Ignore(t => t.RoleName);

    builder.HasOne(p => p.Role); 
  }
}

public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
{
  public void Configure(EntityTypeBuilder<UserPermission> builder)
  {
    builder.ToTable("iam_user_permissions");
    builder.Property(t => t.UserId).HasColumnName("user_id");
    builder.Property(t => t.PermissionId).HasColumnName("permission_id");
    builder.Property(t => t.Permissions).HasColumnName("permission_flag");
    builder.HasKey(p => new { p.UserId, p.PermissionId });
    builder.Ignore(t => t.PermissionName);

    builder.HasOne(p => p.Permission);
  }
}

public class AuthTokenConfiguration : IEntityTypeConfiguration<AuthToken>
{
  public void Configure(EntityTypeBuilder<AuthToken> builder)
  {
    builder.ToTable("iam_tokens");
    builder.Property(t => t.UserId).HasColumnName("user_id").HasMaxLength(128);
    builder.Property(t => t.ExpiryDate).HasColumnName("expiry_date");
    builder.Property(t => t.Token).HasColumnName("token").HasMaxLength(4000);
    builder.HasKey(p => p.UserId);
  }
}