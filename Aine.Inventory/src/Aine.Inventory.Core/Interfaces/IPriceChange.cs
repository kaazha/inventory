namespace Aine.Inventory.Core.Interfaces;

public interface IPriceChange
{
  public int ProductId { get; }

  public DateTime EffectiveDate { get;}

  public DateTime? EndDate { get;}

  public double ListPrice { get;}

  public string? Notes { get;}

  public string? ChangedBy { get; }
}
