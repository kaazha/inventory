using System;
using Aine.Inventory.Api.Endpoints.ModelEndpoints;
using Aine.Inventory.Core.ProductModelAggregate;
using Aine.Inventory.Core.TransactionAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.TransactionEndpoints;

public class Get : Endpoint<TransactionRequest, ICollection<ProductTransaction>>
{
  private readonly IReadRepository<ProductTransaction> _repository;

  public Get(IReadRepository<ProductTransaction> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Get("/transactions");
    AllowAnonymous();
  }

  public override async Task<ICollection<ProductTransaction>> ExecuteAsync(TransactionRequest request, CancellationToken cancellationToken)
  {
    var specification = new TransactionSpecification(request.ProductId, request.TransactionType);
    var transactions = await _repository.ListAsync(specification, cancellationToken);
    return transactions;
  }
}
