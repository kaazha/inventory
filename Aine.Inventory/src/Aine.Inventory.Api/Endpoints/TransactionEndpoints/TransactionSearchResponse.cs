using Aine.Inventory.Core.TransactionAggregate;
using Newtonsoft.Json;

namespace Aine.Inventory.Api.Endpoints.TransactionEndpoints;

public class TransactionSearchResponse
{
  public TransactionSearchOptions SearchOptions { get; set; } = default!;
  [JsonProperty]
  public int? Count => Transactions?.Count;
  public ICollection<TransactionDto> Transactions { get; set; } = default!;
}