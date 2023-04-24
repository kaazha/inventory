namespace Aine.Inventory.Core;

public interface ISettings
{
  IReadOnlyDictionary<string, string> All { get; }
  int DecimalPlaces { get; } 

  string? Get(string key);
  string? this[string key] { get; }
  int GetInt32(string key, int defaultValue);
  Task Update(IEnumerable<KeyValuePair<string, string>> updates, CancellationToken cancellationToken);
  Task Update(string key, string value);
}
