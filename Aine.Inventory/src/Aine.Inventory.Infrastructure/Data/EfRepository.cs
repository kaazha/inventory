using Ardalis.Specification.EntityFrameworkCore;
using Aine.Inventory.SharedKernel.Interfaces;
using Aine.Inventory.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Aine.Inventory.Infrastructure.Data;

[Inject(TypeFinder = typeof(AggregatesFinder))]
// inherit from Ardalis.Specification type
public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T>
  where T : EntityBase<int>, IAggregateRoot
{
  private AppDbContext _context;

  public EfRepository(AppDbContext dbContext) : base(dbContext)
  {
    _context = dbContext;
  }

  public virtual async Task<int> DeleteByIdAsync(int id, CancellationToken cancellationToken)
  {
    return await _context.Set<T>().Where(c => c.Id == id).ExecuteDeleteAsync();
  }
}
