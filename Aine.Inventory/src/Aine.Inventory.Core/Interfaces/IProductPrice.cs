namespace Aine.Inventory.Core.Interfaces;

public interface IProductPrice : IPriceChange
{
  int Id { get; }
  //int ProductId { get; }
  //DateTime EffectiveDate { get; }
  //DateTime? EndDate { get; }
  //DateTime? DateChanged { get; }
  //string? ChangedBy { get; }
  //double ListPrice { get; }
  double? PriceChange { get; }
  //string? Notes { get; }
}