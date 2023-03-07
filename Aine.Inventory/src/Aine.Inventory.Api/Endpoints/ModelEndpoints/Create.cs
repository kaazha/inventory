using Aine.Inventory.Core.ProductModelAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Aine.Inventory.Api.Endpoints.ModelEndpoints;

[HttpPost("/models")]
[AllowAnonymous]
public class Create : Endpoint<CreateModelRequest, ProductModel>
{
  private readonly IRepository<ProductModel> _repository;

  public Create(IRepository<ProductModel> repository)
  {
    _repository = repository;
  }

  public override async Task HandleAsync(
    CreateModelRequest request,
    CancellationToken cancellationToken)
  {
    var newModel = new ProductModel(0, request.Name, request.Description);
    var createdItem = await _repository.AddAsync(newModel, cancellationToken);

    await SendAsync(createdItem, StatusCodes.Status201Created, cancellation: cancellationToken);
  }
}
