namespace Aine.Inventory.SharedKernel;

public static class Guard
{
  public static class Against
  {
    public static string NullOrEmpty(string value, string argName)
    {
      if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(argName);
      return value;
    }

    public static void Null(object value, string argName) => ArgumentNullException.ThrowIfNull(value, argName);
  }
}

