using Aine.Inventory.Core.CategoryAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Aine.Inventory.Api.Endpoints.CategoryEndpoints;

[FastEndpoints.HttpPut("/categories/{id}/subcategories")]
[AllowAnonymous]
public class UpdateSubCategory : Endpoint<UpdateSubCategoryRequest>
{
  private readonly IRepository<ProductSubCategory> _repository;

  public UpdateSubCategory(IRepository<ProductSubCategory> repository)
  {
    _repository = repository;
  }

  [SwaggerOperation(
    Summary = "Updates a Product SubCategory",
    Description = "Updates a new Product SubCategory",
    OperationId = "Category.UpdateSubCategory",
    Tags = new[] { "CategoryEndpoints" })
  ]
  public override async Task HandleAsync(
    UpdateSubCategoryRequest request,
    CancellationToken cancellationToken)
  {
    var subCategory = new ProductSubCategory(request);
    await _repository.UpdateAsync(subCategory, cancellationToken);
    await SendOkAsync(subCategory, cancellationToken);
  }
}