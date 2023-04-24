namespace Aine.Inventory.Core.TransactionAggregate;
using System.Text.Json.Serialization;

public class TransactionDto
{
  [JsonPropertyName("id")]
  public int TransactionId { get; set; }
  public int ProductId { get; set; }
  public string? ProductNumber { get; set; }
  public string? ProductName { get; set; }
  public DateTime TransactionDate { get; set; }
  public string? TransactionType { get; set; }
  public string? ReferenceNumber { get; set; }
  public int Quantity { get; set; }
  public double? TotalCost { get; set; }
  public string? Notes { get; set; }
  public string? CreatedBy { get; set; }
  public DateTime DateCreated { get; set; }
  public string? ModifiedBy { get; set; }
  public DateTime? ModifiedDate { get; set; }
}
