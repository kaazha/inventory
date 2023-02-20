using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.Core.ProductAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.ProductEndpoints;

public class Create : Endpoint<CreateProductRequest, IProduct>
{
  private readonly IProductRepository _repository;

  public Create(IProductRepository repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Post("/products");
    AllowAnonymous();
  }

  public override async Task HandleAsync(
    CreateProductRequest request,
    CancellationToken cancellationToken)
  {
    var createdItem = await _repository.CreateProductAsync(request, cancellationToken);
    await SendAsync(createdItem);
  }
}
