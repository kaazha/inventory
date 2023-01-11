namespace Aine.Inventory.SharedKernel.Guard;

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
