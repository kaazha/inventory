namespace Aine.Inventory.Core.Interfaces;

public interface IProductService
{
  Task<IProduct> SaveAsync(IProduct product);
}