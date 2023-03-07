using System;
using Aine.Inventory.SharedKernel;
using Aine.Inventory.SharedKernel.Guard;
using Aine.Inventory.SharedKernel.Interfaces;

namespace Aine.Inventory.Core.TransactionAggregate;

public class ProductTransaction : EntityBase<int>, IAggregateRoot
{
  public static ICollection<string> TransactionTypes = Enum.GetNames<TransactionTypes>().ToHashSet();

  private ProductTransaction() { }

  public int ProductId { get; private set; }
  public DateTime TransactionDate { get; private set; }
  public string TransactionType { get; private set; } = default!;
  public string? ReferenceNumber { get; set; }
  public int Quantity { get; set; }
  public double? TotalCost { get; set; }
  public string? Notes { get; set; }
  public string? CreatedBy { get; set; }
  public DateTime DateCreated { get; set; } = DateTime.UtcNow;
  public string? ModifiedBy { get; set; }
  public DateTime? ModifiedDate { get; set; }

  public ProductAggregate.Product? Product { get; private set; }

  public static ProductTransaction Create(
      int id,
      int productId,
      string transactionType,
      DateTime transactionDate,
      string? referenceNumber,
      int quantity,
      double? totalCost,
      string? notes,
      string? createdBy
      )
  {
    GuardModel.Against.ZeroOrNegative(productId, "Invalid Product Id.");
    GuardModel.Against.Negative(quantity, nameof(quantity));
    GuardModel.IsOneOf(transactionType, TransactionTypes, "Invalid transaction type.");

    return new ProductTransaction
    {
      Id = id,
      ProductId = productId,
      TransactionType = transactionType,
      TransactionDate = transactionDate,
      ReferenceNumber = referenceNumber,
      Quantity = quantity,
      TotalCost = totalCost,
      Notes = notes,
      CreatedBy = createdBy
    };
  }
}

public enum TransactionTypes
{
  Sales,
  Inflow,
  Outflow
}