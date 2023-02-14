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
  private readonly IRepository<Product> _repository;

  public Update(IRepository<Product> repository)
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
    var product = Product.Create(request);
    await _repository.UpdateAsync(product, cancellationToken);
    await SendAsync(product);
  }
}
