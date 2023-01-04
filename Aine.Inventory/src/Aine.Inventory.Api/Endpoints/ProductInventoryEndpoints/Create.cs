using Aine.Inventory.Core.ProductInventoryAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Aine.Inventory.Api.Endpoints.ProductInventoryEndpoints;

[FastEndpoints.HttpPost("/products/inventory")]
[AllowAnonymous]
public class Create : Endpoint<CreateProductInventoryRequest, CreateProductInventoryResponse>
{
  private readonly IRepository<ProductInventory> _repository;

  public Create(IRepository<ProductInventory> repository)
  {
    _repository = repository;
  }

  [SwaggerOperation(
    Summary = "Inserts a new Product Inventory",
    Description = "Inserts a new Product Inventory",
    OperationId = "ProductInventory.Create",
    Tags = new[] { "ProductInventoryEndpoints" })
  ]
  public override async Task<ActionResult<CreateProductInventoryResponse>> HandleAsync(
    CreateProductInventoryRequest request,
    CancellationToken cancellationToken)
  {
    var inventory = ProductInventory.Create(request);
    var createdItem = await _repository.AddAsync(inventory, cancellationToken);
    var response = new CreateProductInventoryResponse(createdItem);

    return  new CreatedResult($"/products/{request.ProductId}/inventory/{createdItem.Id}", response);
  }
}
