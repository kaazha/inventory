using System;
using System.Collections.Immutable;
using System.Runtime;
using System.Threading;

namespace Aine.Inventory.Core;

public abstract class AbstractSettings : ISettings
{
  private IDictionary<string, string>? _settings;

  protected IDictionary<string, string> Settings
  {
    get => _settings ??= LoadSettings();
    set => _settings = value;
  }

  protected abstract IDictionary<string, string> LoadSettings();
  protected void Reset() => _settings = null;
  public IReadOnlyDictionary<string, string> All => Settings.ToImmutableDictionary();
  public string? this[string key] => Settings.TryGetValue(key, out var value) ? value : null;
  public string? Get(string key) => this[key];
  public int GetInt32(string key, int defaultValue) => int.TryParse(this[key], out var value) ? value : defaultValue;
  public abstract Task Update(string key, string value);
  public abstract Task Update(IEnumerable<KeyValuePair<string, string>> updates, CancellationToken cancellationToken);
}
