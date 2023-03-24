using Aine.Inventory.Core;
using Aine.Inventory.Infrastructure.Data;
using Aine.Inventory.Infrastructure.Data.Config;
using Aine.Inventory.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Aine.Inventory.Infrastructure;

[Inject(Scope = InstanceScope.Singleton)]
public class DefaultSettingProvider : AbstractSettings
{
  private Func<AppDbContext> _contextFactory;

  public DefaultSettingProvider(Func<AppDbContext> dbContextFactory)
  {
    _contextFactory = dbContextFactory;
  }

  protected override IDictionary<string, string> LoadSettings()
  {
    var allSettings = _contextFactory()
                      .Set<Setting>()
                      .ToDictionary(s => s.Name, s => s.Value, StringComparer.OrdinalIgnoreCase);
    return allSettings;
  }

  public override async Task Update(string key, string value)
  {
    if (string.IsNullOrEmpty(key))
      throw new ArgumentException("Setting key is required!");
    await _contextFactory().Set<Setting>()
        .Where(s => s.Name == key)
        .ExecuteUpdateAsync(s => s.SetProperty(p => p.Value,  value));
  }

  public override async Task Update(IEnumerable<KeyValuePair<string, string>> updates, CancellationToken cancellationToken)
  {
    var dbSet = _contextFactory().Set<Setting>();
    foreach (var (key, value) in updates)
    { 
        await dbSet.Where(s => s.Name == key)
          .ExecuteUpdateAsync(s => s.SetProperty(p => p.Value, value), cancellationToken);
      Settings[key] = value;
    }
  }
}
