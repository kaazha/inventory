using System.Threading;
using Aine.Inventory.Core.CategoryAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Aine.Inventory.Api.Endpoints.CategoryEndpoints;

public class DeleteCategory : EndpointWithoutRequest
{ 
  private readonly IRepository<ProductCategory> _repository;

  public DeleteCategory(IRepository<ProductCategory> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Delete("/categories/{categoryId}");
    AllowAnonymous();
    DontCatchExceptions();
  }

  [SwaggerOperation(
    Summary = "Deletes a Product Category",
    Description = "Deletes a specified Product Category",
    OperationId = "Category.Delete",
    Tags = new[] { "CategoryEndpoints" })
  ]
  public override async Task HandleAsync(
    CancellationToken cancellationToken)
  {
    var id = Route<int>("categoryId", true);
    var deleted = await _repository.DeleteByIdAsync(id, cancellationToken);
    if (deleted <= 0) await SendNotFoundAsync(cancellationToken);
    await SendOkAsync(cancellationToken);
  }
}
