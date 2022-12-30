using Ardalis.Specification;

namespace Aine.Inventory.SharedKernel.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}

