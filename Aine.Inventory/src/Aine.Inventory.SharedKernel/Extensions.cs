using System;
using System.Text;

namespace Aine.Inventory.SharedKernel;

public static class Extensions
{

  public static void AddDomainEvent(this EntityBase entity, DomainEventBase domainEvent)
  {
    entity.RegisterDomainEvent(domainEvent);
  }

  public static void ForEachItem<T>(this IEnumerable<T>? list, Action<T> action)
  {
    if (list == null) return;
    foreach (var item in list)
      action(item);
  }

  public static string Join<T>(this IEnumerable<T>? values, char separator = ',')
  {
    if (values == null || !values.Any()) return string.Empty;
    return new StringBuilder().AppendJoin(separator, values).ToString();
  }

  public static string Join<T>(this IEnumerable<T>? values, string separator = ",")
  {
    if (values == null || !values.Any()) return string.Empty;
    return new StringBuilder().AppendJoin(separator, values).ToString();
  }
}

