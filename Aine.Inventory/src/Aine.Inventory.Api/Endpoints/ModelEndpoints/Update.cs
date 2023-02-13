using System.ComponentModel.DataAnnotations;
using Aine.Inventory.Core.ProductModelAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace Aine.Inventory.Api.Endpoints.ModelEndpoints;

[FastEndpoints.HttpPut("/models")]
[AllowAnonymous]
public class Update : Endpoint<UpdateModelRequest>
{
  private readonly IRepository<ProductModel> _repository;

  public Update(IRepository<ProductModel> repository)
  {
    _repository = repository;
  }

  [SwaggerOperation(
    Summary = "Updates a Product Model",
    Description = "Updates a new Product Model",
    OperationId = "Model.Update",
    Tags = new[] { "ModelEndpoints" })
  ]
  public override async Task HandleAsync(
    UpdateModelRequest request,
    CancellationToken cancellationToken)
  {
    var model = new ProductModel(request.Id, request.Name, request.Description);
    await _repository.UpdateAsync(model, cancellationToken);
    await SendOkAsync(request);
  }
}

public class UpdateModelRequest
{
  public int Id { get; set; }

  [Required]
  public string Name { get; set; } = default!;
  public string? Description { get; set; }
}