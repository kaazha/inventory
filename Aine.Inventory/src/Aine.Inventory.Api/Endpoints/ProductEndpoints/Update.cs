using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.Core.ProductAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using FluentValidation;

namespace Aine.Inventory.Api.Endpoints.ProductEndpoints;

public class Update : Endpoint<UpdateProductRequest, IProduct>
{
  private readonly IProductRepository _repository;

  public Update(IProductRepository repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Put("/products");
    //AllowAnonymous();
  }

  public override async Task HandleAsync(
    UpdateProductRequest request,
    CancellationToken cancellationToken)
  {
    var product = await _repository.UpdateProductAsync(request, User.AsUser(), cancellationToken);
    await SendAsync(product);
  }
}
