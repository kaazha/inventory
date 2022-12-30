using Microsoft.EntityFrameworkCore;

namespace Aine.Inventory.Infrastructure.Data;

public class MigrationContext : AppDbContext
{
  public MigrationContext() : base(new DbContextOptionsBuilder<AppDbContext>().UseSqlite("Data Source=inventory2.db").Options, default)
  {
  }
}

