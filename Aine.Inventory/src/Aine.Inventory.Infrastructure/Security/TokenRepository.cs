using System;
using Aine.Inventory.Core.Helpers;
using Aine.Inventory.Core.Interfaces;
using Aine.Inventory.Infrastructure.Data;
using Aine.Inventory.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace Aine.Inventory.Infrastructure.Security;

[Inject]
public class TokenRepository : ITokenRepository
{
  private AppDbContext _context;

  public TokenRepository(AppDbContext dbContext)
  {
    _context = dbContext;
  }

  public async Task StoreToken(string userId, DateTime expiryDate, string token)
  {
    var dbSet = _context.Set<AuthToken>();
    await dbSet.Where(a => a.UserId == userId).ExecuteDeleteAsync();
    var tokenInfo = new AuthToken
    {
      UserId = userId,
      ExpiryDate = expiryDate,
      Token = token
    };
    dbSet.Add(tokenInfo);
    await _context.SaveChangesAsync();
  }

  public async Task<bool> TokenIsValid(string userId, string token)
  {
    return await _context.Set<AuthToken>().AnyAsync(t =>
                         t.UserId == userId &&
                         t.Token == token &&
                         t.ExpiryDate >= DateTime.UtcNow);
  }
}

