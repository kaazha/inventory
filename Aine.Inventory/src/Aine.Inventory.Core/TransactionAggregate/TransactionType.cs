using System.Reflection;
using Aine.Inventory.SharedKernel.Guard;

namespace Aine.Inventory.Core;

public readonly struct TransactionType
{
  private TransactionType(string code, string name) => (Code, Name) = (code, name);

  /// <summary>
  /// Descriptive name
  /// </summary>
  public string Name { get; }

  /// <summary>
  /// One letter code
  /// </summary>
  public string Code { get; }

  public static TransactionType Sales { get; } = new("S", "Sales");
  public static TransactionType Inflow { get; }  = new("I", "Inflow");
  public static TransactionType Outflow { get; }  = new("O", "Outflow");
  public static TransactionType WorkOrder { get; } = new("W", "Work Order");
  public static TransactionType Purchase { get; }  = new("P", "Purchase");
  private static List<TransactionType>? _all;

  public static IReadOnlyCollection<TransactionType> All => _all ??= GetAllTypes();  

  public override string ToString() => $"{Name}({Code})";

  public static TransactionType? FromName(string transactionType)
  {
    Guard.Against.NullOrEmpty(transactionType, nameof(transactionType));
    return All.FirstOrDefault(t =>
            string.Equals(transactionType, t.Code, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(transactionType, t.Name, StringComparison.OrdinalIgnoreCase)
            );
  }

  private static List<TransactionType> GetAllTypes()
  {
    var thisType = typeof(TransactionType);
    var values =  thisType.GetProperties()
                .Where(p => p.PropertyType == thisType)
                .Select(p => (TransactionType)p.GetValue(null)!)
                .ToList();

    return values;
  }
}

