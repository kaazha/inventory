using System.Threading;
using Aine.Inventory.Core.CategoryAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Aine.Inventory.Api.Endpoints.CategoryEndpoints;

public class DeleteSubCategory : EndpointWithoutRequest
{ 
  private readonly IRepository<ProductSubCategory> _repository;

  public DeleteSubCategory(IRepository<ProductSubCategory> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Delete("/categories/{categoryId}/{subcategories}/{subcategoryId}",
      "/{subcategories}/{subcategoryId}"
      );
    AllowAnonymous();
    DontCatchExceptions();
  }

  [SwaggerOperation(
    Summary = "Deletes a Product SubCategory",
    Description = "Deletes a specified Product SubCategory",
    OperationId = "Category.Delete",
    Tags = new[] { "CategoryEndpoints" })
  ]
  public override async Task HandleAsync(
    CancellationToken cancellationToken)
  {
    var id = Route<int>("subcategoryId", true);
    var deleted = await _repository.DeleteByIdAsync(id, cancellationToken);
    if (deleted <= 0) await SendNotFoundAsync(cancellationToken);
    await SendOkAsync(cancellationToken);
  }
}
