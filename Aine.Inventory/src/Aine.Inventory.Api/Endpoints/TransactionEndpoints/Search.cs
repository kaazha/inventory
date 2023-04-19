using Aine.Inventory.Core.TransactionAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.TransactionEndpoints;

public class Search : Endpoint<TransactionSearchOptions, TransactionSearchResponse>
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
    Claims("UserId", "UserName");
    Roles("Admin", "Manager");
    Permissions("View Transactions");
  }

  public override async Task<TransactionSearchResponse> ExecuteAsync(TransactionSearchOptions searchOptions, CancellationToken cancellationToken)
  {
    var specification = new TransactionSearchSpecification(searchOptions);
    var transactions = await _repository.ListAsync(specification, cancellationToken);
    return new TransactionSearchResponse { Transactions = transactions, SearchOptions = searchOptions };
  }
}

