using System;
namespace Aine.Inventory.SharedKernel;

public static class Extensions
{

  public static void ForEachItem<T>(this IEnumerable<T> list, Action<T> action)
  {
    if (list == null) return;
    foreach (var item in list)
      action(item);
  }
}

