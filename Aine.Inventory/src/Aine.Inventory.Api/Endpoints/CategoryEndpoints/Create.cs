using Aine.Inventory.Core.CategoryAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Aine.Inventory.Api.Endpoints.CategoryEndpoints;

[FastEndpoints.HttpPost("/categories")]
[AllowAnonymous]
public class Create : Endpoint<CreateCategoryRequest, ProductCategory>
{
  private readonly IRepository<ProductCategory> _repository;

  public Create(IRepository<ProductCategory> repository)
  {
    _repository = repository;
  }

  public override async Task HandleAsync(
    CreateCategoryRequest request,
    CancellationToken cancellationToken)
  {
    var newCategory = new ProductCategory(request);
    var createdItem = await _repository.AddAsync(newCategory, cancellationToken);
    await SendAsync(createdItem, StatusCodes.Status201Created, cancellation: cancellationToken);
  }
}
