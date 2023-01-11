namespace Aine.Inventory.SharedKernel.Guard;

public static class Guard
{
  public static class Against
  {
    public static string NullOrEmpty(string? value, string argName)
    {
      if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(argName);
      return value;
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
  }
}
