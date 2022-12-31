using System.Text;
using Aine.Inventory.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aine.Inventory.Infrastructure.Data.Config;

public abstract class EntityConfigurationBase<T> where T : class
{

  public void ConfigureColumnNames(EntityTypeBuilder<T> builder)
  {
    var properties = typeof(T)
      .GetProperties()
      .Where(p => p is { CanRead: true, CanWrite: true } &&
                  Type.GetTypeCode(ActualType(p.PropertyType)) != TypeCode.Object &&
                  !p.IsDefined(typeof(IgnoreMemberAttribute), true));

    foreach (var prop in properties)
    {
      var columnName = ToColumnName(prop.Name);
      builder.Property(prop.Name)?.HasColumnName(columnName);
    }

    static Type ActualType(Type type) => Nullable.GetUnderlyingType(type) ?? type;
  }

  public static string ToColumnName(string name)
  {
    var chars = new StringBuilder();
    for (var i = 0; i < name.Length; i++)
    {
      if (char.IsUpper(name[i]))
      {
        if (i > 0) chars.Append('_');
        chars.Append(char.ToLower(name[i]));
        continue;
      }

      chars.Append(name[i]);
    }

    return chars.ToString();
  }
}

