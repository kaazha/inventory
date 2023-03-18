using Aine.Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Aine.Inventory.Api;

public class MigrationContext : AppDbContext
{
  public MigrationContext() : base(new DbContextOptionsBuilder<AppDbContext>().UseSqlite("Data Source=inventory2.db").Options, default)
  {
  }
}

