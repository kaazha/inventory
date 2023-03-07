namespace Aine.Inventory.Core.Interfaces;

public record ProductInfo(
  int ProductId,
  string ProductNumber,
  string ProductName,
  string? CategoryName
  );
