using Aine.Inventory.Core.CategoryAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;

namespace Aine.Inventory.Web.Endpoints.CategoryEndpoints;

public class List : EndpointWithoutRequest<CategoryListResponse>
{
  private readonly IReadRepository<Category> _repository;

  public List(IReadRepository<Category> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Verbs(Http.GET);
    Routes("/categories");
    AllowAnonymous();
  }

  public override async Task<CategoryListResponse> ExecuteAsync(CancellationToken cancellationToken)
  {
    var categories = await _repository.ListAsync(cancellationToken);
    var response = new CategoryListResponse
    {
      Categories = categories
        .Select(c => new CategoryRecord(c.Id, c.Name, c.Description))
        .ToList()
    };

    return response;
  }
}

