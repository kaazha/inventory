using System;
using Aine.Inventory.Core.TransactionAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.TransactionEndpoints;

public class Search : Endpoint<TransactionSearchOptions, ICollection<TransactionDto>>
{
  private readonly IReadRepository<ProductTransaction> _repository;

  public Search(IReadRepository<ProductTransaction> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Get("/transactions/search");
    Post("/transactions/search");
    AllowAnonymous();
  }

  public override async Task<ICollection<TransactionDto>> ExecuteAsync(TransactionSearchOptions searchOptions, CancellationToken cancellationToken)
  {
    var specification = new TransactionSearchSpecification(searchOptions);
    var transactions = await _repository.ListAsync(specification, cancellationToken);
    return transactions;
  }
}

