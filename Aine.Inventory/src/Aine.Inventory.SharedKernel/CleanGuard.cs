namespace Aine.Inventory.SharedKernel;

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

public class GuardModel
{
  public static class Against
  {
    public static string NullOrEmpty(string? value, string message)
    {
      if (string.IsNullOrEmpty(value)) throw new ModelValidationException(message);
      return value;
    }

    public static int ZeroOrNegative(int value, string message)
    {
      if (value <= 0) throw new ModelValidationException(message);
      return value;
    }

    public static string TooLong(string value, int maxLength, string message)
    {
      if (value.Length > maxLength) throw new ModelValidationException(message);
      return value;
    }

    public static int? Negative(int? value, string message)
    {
      if (value < 0) throw new ModelValidationException(message);
      return value;
    }

    public static float? Negative(float? value, string message)
    {
      if (value < 0) throw new ModelValidationException(message);
      return value;
    }

    public static double? Negative(double? value, string message)
    {
      if (value < 0) throw new ModelValidationException(message);
      return value;
    }
  }
}

public class ModelValidationException : ApplicationException
{
  public ModelValidationException(string message) : base(message) { }
}
