namespace Aine.Inventory.SharedKernel.Guard;

public static class Guard
{ 
  public static class Against
  {
    public static string NullOrEmpty(string? value, string paramName)
    {
      if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(paramName);
      return value;
    }

    public static void NullOrEmpty(string? value, Action action)
    {
      if (string.IsNullOrEmpty(value)) action();
    }

    public static void NullOrEmpty<T>(IEnumerable<T>? collection, string paramName, string? message = default)
    {
      if (collection == null || !collection.Any())
        throw new ArgumentNullException(paramName, message);
    }

    public static void NullOrEmpty<T>(IEnumerable<T>? collection, Action action)
    {
      if (collection == null || !collection.Any()) action();
    }

    public static void Null(object value, string argName) => ArgumentNullException.ThrowIfNull(value, argName);

    public static float? Negative(float? value, string argName)
    {
      if (value < 0) throw new ArgumentException($"{argName} can't be negative!", argName);
      return value;
    }

    public static double? Negative(double? value, string argName)
    {
      if (value < 0) throw new ArgumentException($"{argName} can't be negative!", argName);
      return value;
    }

    public static void NotOneOf<T>(T value, ICollection<T> values, Action<T> action)
    {
      if (!values.Contains(value)) action(value);
    }
  }
}
