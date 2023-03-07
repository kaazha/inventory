using FastEndpoints;

namespace Aine.Inventory.Api.Endpoints.TransactionEndpoints;

public class TransactionRequest
{
  [QueryParam]
  public int? ProductId { get; set; }

  [QueryParam]
  public string? TransactionType { get; set; }
}
