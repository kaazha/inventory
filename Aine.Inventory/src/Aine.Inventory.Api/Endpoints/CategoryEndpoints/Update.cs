using Aine.Inventory.Core.CategoryAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Aine.Inventory.Api.Endpoints.CategoryEndpoints;

[FastEndpoints.HttpPut("/categories")]
[AllowAnonymous]
public class Update : Endpoint<UpdateCategoryRequest>
{
  private readonly IRepository<ProductCategory> _repository;

  public Update(IRepository<ProductCategory> repository)
  {
    _repository = repository;
  }

  public override async Task HandleAsync(
    UpdateCategoryRequest request,
    CancellationToken cancellationToken)
  {
    var category = new ProductCategory(request);
    await _repository.UpdateAsync(category, cancellationToken);
    await SendOkAsync(category);
  }
}
