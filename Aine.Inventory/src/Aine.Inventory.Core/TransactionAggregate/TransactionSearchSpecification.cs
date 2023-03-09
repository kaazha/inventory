using System;
using Aine.Inventory.Core.ProductAggregate;
using System.Linq.Expressions;
using Ardalis.Specification;
using Aine.Inventory.SharedKernel;
using Aine.Inventory.Core.ProductAggregate.Specifications;
using System.Transactions;
using static System.Math;

namespace Aine.Inventory.Core.TransactionAggregate;

public class TransactionSearchSpecification : Specification<ProductTransaction, TransactionDto>
{
  public TransactionSearchSpecification(TransactionSearchOptions options)
  {
    ArgumentNullException.ThrowIfNull(options, "TransactionSearchOptions");
    if (!options.IsValid()) throw new ArgumentException("At least one transaction search option must be specified!");
    var transactionTypes = options.TransactionTypes?.Select(type => type switch
    {
      "Sales" => "S",
      "Inflow" => "P",
      "Outflow" => "W",
      _ => type
    })?.ToArray() ?? Array.Empty<string>();

    Expression<Func<ProductTransaction, bool>> predicate = p => true;
    predicate = predicate.AndAlso(p => p.ProductId == options.ProductId, options.ProductId > 0)
                         .AndAlso(p => p.Product!.ProductNumber.Contains(options.ProductNumber!), !string.IsNullOrEmpty(options.ProductNumber))
                         .AndAlso(p => transactionTypes.Contains(p.TransactionType), transactionTypes != null && transactionTypes.Any())
                         .AndAlso(p => p.ReferenceNumber!.Contains(options.ReferenceNumber!), !string.IsNullOrEmpty(options.ReferenceNumber))
                         .AndAlso(p => p.TransactionDate >= options.TransactionDateStart, options.TransactionDateStart != null) // inludes start date
                         .AndAlso(p => p.TransactionDate < options.TransactionDateEnd, options.TransactionDateEnd != null);  // excludes end date
    Query
        .Select(t => new TransactionDto
        {
          TransactionId = t.Id,
          TransactionType = t.TransactionType,
          TransactionDate = t.TransactionDate,
          ProductId = t.ProductId,
          ProductNumber = t.Product!.ProductNumber,
          ReferenceNumber = t.ReferenceNumber,
          ProductName = t.Product.Name,
          TotalCost = t.TotalCost != null ? Round(t.TotalCost.Value, 2) : null,
          Quantity = t.Quantity,
          CreatedBy = t.CreatedBy,
          DateCreated = t.DateCreated,
          ModifiedBy = t.ModifiedBy,
          ModifiedDate = t.ModifiedDate,
          Notes = t.Notes
        })
        .Where(predicate)
        .OrderBy(p => p.ProductId)
        .ThenBy(p => p.TransactionType)
        .ThenByDescending(p => p.TransactionDate)
        ;//.Limit();
  }
}

public class TransactionDto
{
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

public class TransactionSearchOptions
{
  public int? ProductId { get; set; }
  public string? ProductNumber { get; set; }
  public DateTime? TransactionDateStart { get; set; }
  public DateTime? TransactionDateEnd { get; set; }
  public string? ReferenceNumber { get; set; }
  public string[]? TransactionTypes { get; set; }

  public bool IsValid() =>
    ProductId > 0 ||
    !string.IsNullOrEmpty(ProductNumber) ||
    !string.IsNullOrEmpty(this.ReferenceNumber) ||
   TransactionTypes != null ||
    TransactionDateStart is { } ||
    (TransactionDateEnd is { } && (TransactionDateStart is null || TransactionDateEnd >= TransactionDateStart));
}
