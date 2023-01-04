namespace Aine.Inventory.Core.Interfaces;

public interface IInventory
{
  string? Bin { get; }
  int LocationId { get; }
  int ProductId { get; }
  int Quantity { get; }
  string? Shelf { get; }
}
