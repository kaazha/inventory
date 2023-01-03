using Aine.Inventory.Core.CategoryAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Aine.Inventory.Api.Endpoints.CategoryEndpoints;

[FastEndpoints.HttpPost("/categories")]
[AllowAnonymous]
public class Create : Endpoint<CreateCategoryRequest, CreateCategoryResponse>
{
  private readonly IRepository<ProductCategory> _repository;

  public Create(IRepository<ProductCategory> repository)
  {
    _repository = repository;
  }

  [SwaggerOperation(
    Summary = "Creates a new Product Category",
    Description = "Creates a new Product Category",
    OperationId = "Category.Create",
    Tags = new[] { "CategoryEndpoints" })
  ]
  public override async Task HandleAsync(
    CreateCategoryRequest request,
    CancellationToken cancellationToken)
  {
    var newCategory = new ProductCategory(request.Name, request.Description);
    var createdItem = await _repository.AddAsync(newCategory, cancellationToken);
    var response = new CreateCategoryResponse(createdItem.Id, createdItem.Name);

    await SendAsync(response, StatusCodes.Status201Created, cancellation: cancellationToken);
  }
}
