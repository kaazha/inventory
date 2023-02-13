using System.ComponentModel.DataAnnotations;
using Aine.Inventory.Core.ProductModelAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Aine.Inventory.Api.Endpoints.ModelEndpoints;

public class DeleteModel : EndpointWithoutRequest
{ 
  private readonly IRepository<ProductModel> _repository;

  public DeleteModel(IRepository<ProductModel> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Delete("/models/{modelId}");
    AllowAnonymous();
    DontCatchExceptions();
  }

  [SwaggerOperation(
    Summary = "Deletes a Product Model",
    Description = "Deletes a specified Product Model",
    OperationId = "Model.Delete",
    Tags = new[] { "ModelEndpoints" })
  ]
  public override async Task HandleAsync(
    CancellationToken cancellationToken)
  {
    var id = Route<int>("modelId", true);
    var deleted = await _repository.DeleteByIdAsync(id, cancellationToken);
    if (deleted <= 0) await SendNotFoundAsync(cancellationToken);
    await SendOkAsync(cancellationToken);
  }
}
