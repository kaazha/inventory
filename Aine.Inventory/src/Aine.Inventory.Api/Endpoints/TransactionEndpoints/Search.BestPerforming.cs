using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.Core.TransactionAggregate;
using Aine.Inventory.SharedKernel.Interfaces;
using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.TransactionEndpoints;

public class SearchBestPerforming : Endpoint<BestPerformingTransactionOptions, IEnumerable<ProductTransactionSummary>>
{
  private readonly ISummaryRepository _repository;

  public SearchBestPerforming(ISummaryRepository repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Post("/products/best-performing");
    Claims("UserId", "UserName");
    Roles("Admin", "Manager");
    Permissions("View Transactions");
  }

  public override async Task<IEnumerable<ProductTransactionSummary>> ExecuteAsync(BestPerformingTransactionOptions searchOptions, CancellationToken cancellationToken)
  {
    var options = searchOptions ?? new BestPerformingTransactionOptions();
    var transactions = await _repository.GetSummary(options.ToTransactionSearchOptions(), cancellationToken);
    return transactions;
  }
}
