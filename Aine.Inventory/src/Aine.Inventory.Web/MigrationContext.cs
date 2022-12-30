using System;
namespace Aine.Inventory.Infrastructure.Data;

public class MigrationContext : AppDbContext
{
  public MigrationContext() : base(new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder().UseSqlite(""), default)
  {
  }
}

