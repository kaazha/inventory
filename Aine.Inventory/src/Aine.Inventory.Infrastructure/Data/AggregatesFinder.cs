using Aine.Inventory.SharedKernel.Interfaces;
using Aine.Inventory.SharedKernel;

namespace Aine.Inventory.Infrastructure.Data;

public class AggregatesFinder : ITypeEnumerator
{
  public IEnumerable<Type> GetTypes()
  {
    return typeof(CoreMarker).Assembly.GetTypes().Where(type => type.GetInterface(nameof(IAggregateRoot)) is not null);
  }
}
