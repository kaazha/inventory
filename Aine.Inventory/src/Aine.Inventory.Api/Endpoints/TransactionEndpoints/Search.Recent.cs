using Aine.Inventory.Core.TransactionAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.TransactionEndpoints;

public class SearchRecent : Endpoint<RecentTransactionOptions, TransactionSearchResponse>
{
  private readonly IReadRepository<ProductTransaction> _repository;

  public SearchRecent(IReadRepository<ProductTransaction> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Get("/transactions/recent");
    Post("/transactions/recent");
    //AllowAnonymous();
    Claims("UserId", "UserName");
    Roles("Admin", "Manager");
    Permissions("View Transactions");
  }

  public override async Task<TransactionSearchResponse> ExecuteAsync(RecentTransactionOptions searchOptions, CancellationToken cancellationToken)
  {
    var specification = new RecentTransationsSpecification(searchOptions);
    var transactions = await _repository.ListAsync(specification, cancellationToken);
    return new TransactionSearchResponse { Transactions = transactions, SearchOptions = searchOptions.ToTransactionSearchOptions() };
  }
}
