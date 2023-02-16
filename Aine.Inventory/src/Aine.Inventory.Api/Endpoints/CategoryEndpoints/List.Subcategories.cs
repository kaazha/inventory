using Aine.Inventory.Core.CategoryAggregate;
using Aine.Inventory.Core.CategoryAggregate.Specifications;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.CategoryEndpoints;

public class ListSubCategories : EndpointWithoutRequest<ICollection<ProductSubCategory>>
{
  private readonly IReadRepository<ProductSubCategory> _repository;

  public ListSubCategories(IReadRepository<ProductSubCategory> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Get("/SubCategories");
    AllowAnonymous();
  }

  public override async Task<ICollection<ProductSubCategory>> ExecuteAsync(CancellationToken cancellationToken)
  {
    return await _repository.ListAsync(cancellationToken);
  }
}