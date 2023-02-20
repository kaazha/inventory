using System;
namespace Aine.Inventory.Core.Interfaces;

public interface IProductPhotoRepository
{
  Task<int> UpdateProductPhotoAsync(int productId, string productPhotoFileName);
}