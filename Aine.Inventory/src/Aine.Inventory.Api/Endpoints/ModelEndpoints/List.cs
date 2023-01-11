using Aine.Inventory.Core.CategoryAggregate;
using Aine.Inventory.Core.CategoryAggregate.Specifications;
using Aine.Inventory.Core.ProductModelAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.ModelEndpoints;

public class List : EndpointWithoutRequest<ICollection<ModelRecord>>
{
  private readonly IReadRepository<ProductModel> _repository;

  public List(IReadRepository<ProductModel> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Verbs(Http.GET);
    Routes("/models");
    AllowAnonymous();
  }

  public override async Task<ICollection<ModelRecord>> ExecuteAsync(CancellationToken cancellationToken)
  {
    var models = await _repository.ListAsync(cancellationToken);
    return models
        .OrderBy(x => x.Name)
        .Select(c => new ModelRecord(c.Id, c.Name, c.Description))
        .ToList();
  }
}

