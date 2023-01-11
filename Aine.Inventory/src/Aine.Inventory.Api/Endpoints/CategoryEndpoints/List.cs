using Aine.Inventory.Core.CategoryAggregate;
using Aine.Inventory.Core.CategoryAggregate.Specifications;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.CategoryEndpoints;

public class List : EndpointWithoutRequest<ICollection<CategoryRecord>>
{
  private readonly IReadRepository<ProductCategory> _repository;

  public List(IReadRepository<ProductCategory> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Verbs(Http.GET);
    Routes("/categories");
    AllowAnonymous();
  }

  public override async Task<ICollection<CategoryRecord>> ExecuteAsync(CancellationToken cancellationToken)
  {
    var categories = await _repository.ListAsync(new AllCategoriesWithSubCategoriesSpecification(), cancellationToken);
    return categories
        .Select(c => new CategoryRecord(c.Id, c.Name, c.Description, c.SubCategories))
        .ToList();
  }
}

