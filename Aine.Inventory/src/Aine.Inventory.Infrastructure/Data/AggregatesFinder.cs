using Aine.Inventory.SharedKernel.Interfaces;
using Aine.Inventory.SharedKernel;

namespace Aine.Inventory.Infrastructure.Data;

public class AggregatesFinder : ITypeEnumerator
{
  public IEnumerable<Type> GetTypes()
  {
    return GetAssemblyTypes<CoreMarker>()
          .Union(
              GetAssemblyTypes<SharedKernelMarker>()
            );
  }

  private static IEnumerable<Type> GetAssemblyTypes<T>() =>
    typeof(T).Assembly.GetTypes().Where(type => type.GetInterface(nameof(IAggregateRoot)) is not null);
}
