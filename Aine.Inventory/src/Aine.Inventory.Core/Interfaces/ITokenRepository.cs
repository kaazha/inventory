using System;
namespace Aine.Inventory.Core.Interfaces;

public interface ITokenRepository
{
  Task StoreToken(string userId, DateTime expiryDate, string token);
  Task<bool> TokenIsValid(string userId, string token);
}