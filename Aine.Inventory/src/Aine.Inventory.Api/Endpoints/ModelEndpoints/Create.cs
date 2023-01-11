using Aine.Inventory.Core.ProductModelAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Aine.Inventory.Api.Endpoints.ModelEndpoints;

[HttpPost("/models")]
[AllowAnonymous]
public class Create : Endpoint<CreateModelRequest, CreateModelResponse>
{
  private readonly IRepository<ProductModel> _repository;

  public Create(IRepository<ProductModel> repository)
  {
    _repository = repository;
  }

  [SwaggerOperation(
    Summary = "Creates a new Product Model",
    Description = "Creates a new Product Model",
    OperationId = "Model.Create",
    Tags = new[] { "ModelEndpoints" })
  ]
  public override async Task HandleAsync(
    CreateModelRequest request,
    CancellationToken cancellationToken)
  {
    var newModel = new ProductModel(request.Name, request.Description);
    var createdItem = await _repository.AddAsync(newModel, cancellationToken);
    var response = new CreateModelResponse(createdItem.Id, createdItem.Name);

    await SendAsync(response, StatusCodes.Status201Created, cancellation: cancellationToken);
  }
}
