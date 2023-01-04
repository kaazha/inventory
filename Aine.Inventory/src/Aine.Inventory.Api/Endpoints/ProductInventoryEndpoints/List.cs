using Aine.Inventory.Core.ProductInventoryAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;
using static Aine.Inventory.Api.Endpoints.ProductInventoryEndpoints.Get;

namespace Aine.Inventory.Api.Endpoints.ProductInventoryEndpoints;

public class Get : Endpoint<ProductInventoryListRequest, ProductInventoryListResponse>
{
  private readonly IReadRepository<ProductInventory> _repository;

  public Get(IReadRepository<ProductInventory> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Verbs(Http.GET);
    Routes("/products/{productId}/inventory/{locationId}",
          "/products/{productId}/inventory"
          );
    AllowAnonymous();
  }

  public override async Task<ProductInventoryListResponse> ExecuteAsync(
    ProductInventoryListRequest request, 
    CancellationToken cancellationToken)
  {
    var specification = new ProductInventorySpecification(request.ProductId, request.LocationId);
    var inventory = await _repository.ListAsync(specification, cancellationToken);
    var response = new ProductInventoryListResponse { Inventory = inventory };
    return response;
  }

  public class ProductInventoryListRequest
  {
    public int? ProductId { get; set;}
    public int? LocationId { get; set; }
  }
}

