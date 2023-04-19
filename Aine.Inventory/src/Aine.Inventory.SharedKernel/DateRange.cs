namespace Aine.Inventory.SharedKernel;

public readonly struct DateRange
{
  public DateRange(DateTime start, DateTime? end)
  {
    if (end.HasValue && start > end)
      throw new ArgumentException("Start date must be before end date.");
    (Start, End) = (start, end);
  }

  public DateRange(DateTime start) => Start = start;

  public DateTime Start { get; }
  public DateTime? End { get; }

  public override string ToString()
  {
    return End.HasValue ? $"{Start} - {End}" : Start.ToString();
  }
}
