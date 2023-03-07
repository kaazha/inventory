using Aine.Inventory.Core.CategoryAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Aine.Inventory.Api.Endpoints.CategoryEndpoints;

[FastEndpoints.HttpPost("/categories/{id}/subcategories")]
[AllowAnonymous]
public class CreateSubCategory : Endpoint<CreateSubCategoryRequest, ISubCategory>
{
  private readonly IRepository<ProductSubCategory> _repository;

  public CreateSubCategory(IRepository<ProductSubCategory> repository)
  {
    _repository = repository;
  }

  public override async Task HandleAsync(
    CreateSubCategoryRequest request,
    CancellationToken cancellationToken)
  {
    var newCategory = new ProductSubCategory(request);
    var createdItem = await _repository.AddAsync(newCategory, cancellationToken);

    await SendAsync(createdItem, StatusCodes.Status201Created, cancellation: cancellationToken);
  }
}