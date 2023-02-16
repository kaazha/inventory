using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.Core.ProductAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Aine.Inventory.Api.Endpoints.ProductEndpoints;

[FastEndpoints.HttpPost("/products")]
[AllowAnonymous]
public class Create : Endpoint<CreateProductRequest, IProduct>
{
  private readonly IProductRepository _repository;

  public Create(IProductRepository repository)
  {
    _repository = repository;
  }

  [SwaggerOperation(
    Summary = "Creates a new Product",
    Description = "Creates a new Product",
    OperationId = "Product.Create",
    Tags = new[] { "ProductEndpoints" })
  ]
  public override async Task HandleAsync(
    CreateProductRequest request,
    CancellationToken cancellationToken)
  {
    var createdItem = await _repository.CreateProductAsync(request, cancellationToken);
    await SendAsync(createdItem);
  }
}
