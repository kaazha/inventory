using Aine.Inventory.Core.CategoryAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Aine.Inventory.Web.Endpoints.CategoryEndpoints;

[FastEndpoints.HttpPost("/categories")]
[AllowAnonymous]
public class Create : Endpoint<CreateCategoryRequest, CreateCategoryResponse>
{
  private readonly IRepository<Category> _repository;

  public Create(IRepository<Category> repository)
  {
    _repository = repository;
  }

  [SwaggerOperation(
    Summary = "Creates a new Product Category",
    Description = "Creates a new Product Category",
    OperationId = "Category.Create",
    Tags = new[] { "CategoryEndpoints" })
  ]
  public override async Task<ActionResult<CreateCategoryResponse>> HandleAsync(
    CreateCategoryRequest request,
    CancellationToken cancellationToken)
  {
    if (string.IsNullOrEmpty(request.Name))
    {
      return new BadRequestObjectResult("Category name is required");
    }

    var newCategory = new Category(request.Name, request.Description);
    var createdItem = await _repository.AddAsync(newCategory, cancellationToken);
    var response = new CreateCategoryResponse(createdItem.Id, createdItem.Name);

    return  new CreatedResult($"/categories/{createdItem.Id}", response);
  }
}
