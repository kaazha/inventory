using Ardalis.Specification.EntityFrameworkCore;
using Aine.Inventory.SharedKernel.Interfaces;
using Aine.Inventory.SharedKernel;

namespace Aine.Inventory.Infrastructure.Data;

[Inject(TypeFinder = typeof(AggregatesFinder))]
// inherit from Ardalis.Specification type
public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T>
  where T : class, IAggregateRoot
{
  public EfRepository(AppDbContext dbContext) : base(dbContext)
  {
  }
}
