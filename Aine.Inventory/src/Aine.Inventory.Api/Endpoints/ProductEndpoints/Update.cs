using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.Core.ProductAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Aine.Inventory.Api.Endpoints.ProductEndpoints;

[FastEndpoints.HttpPut("/products")]
[AllowAnonymous]
public class Update : Endpoint<UpdateProductRequest, IProduct>
{
  private readonly IProductRepository _repository;

  public Update(IProductRepository repository)
  {
    _repository = repository;
  }

  [SwaggerOperation(
    Summary = "Updates a Product",
    Description = "Updates a Product",
    OperationId = "Product.Update",
    Tags = new[] { "ProductEndpoints" })
  ]
  public override async Task HandleAsync(
    UpdateProductRequest request,
    CancellationToken cancellationToken)
  {
    var product = await _repository.UpdateProductAsync(request, cancellationToken);
    await SendAsync(product);
  }
}
