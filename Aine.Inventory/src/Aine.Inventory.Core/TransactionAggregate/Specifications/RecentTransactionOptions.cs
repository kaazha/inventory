namespace Aine.Inventory.Core.TransactionAggregate;

public class RecentTransactionOptions
{
  private const int DEFAULT_LIMIT = 30;

  public DateInterval? Interval { get; init; }
  public RelativeTo? Relative { get; init; }
  public DateTime? ReferenceDate { get; init; }
  public ICollection<string>? TransactionTypes { get; set; }
  /// <summary>
  /// The [max] number of rows to return
  /// </summary>
  public int? Take { get; set; }

  public TransactionSearchOptions ToTransactionSearchOptions()
  {
    var dateRange = ComputeDateRange();
    if (dateRange == null && this.Take == null)
      this.Take = DEFAULT_LIMIT;

    return new()
    {
      TransactionTypes = this.TransactionTypes,
      TransactionDateStart = dateRange?.Start,
      TransactionDateEnd = dateRange?.End,
      Take = this.Take
    };
  }

  private DateRange? ComputeDateRange()
  {
    if (this.Relative == RelativeTo.Since && ReferenceDate.HasValue)
      return new(ReferenceDate.Value, DateTime.Today.AddDays(1));

    return this.Interval switch
    {
      DateInterval.Day => GetRelativeDayDateRange(),
      DateInterval.Week => GetRelativeWeekDateRange(),
      DateInterval.Month => GetRelativeMonthDateRange(),
      DateInterval.Year => GetRelativeYearDateRange(),
      DateInterval.Quarter => GetRelativeQuarterDateRange(),
      _ => null
    };
  }

  private DateRange GetRelativeDayDateRange()
  {
    var today = DateTime.UtcNow;
    return this.Relative switch
    {
      RelativeTo.Current => new(today, today.AddDays(1)),
      RelativeTo.Previous => new(Yesterday(), today),
      _ => new(today),
    };

    DateTime Yesterday() => today.AddDays(-1);
  }

  private DateRange? GetRelativeWeekDateRange()
  {
    var today = DateTime.UtcNow;
    var weekStart = today.AddDays(-(int)today.DayOfWeek);
    return this.Relative switch
    {
      RelativeTo.Current => new(weekStart, today),
      RelativeTo.Previous => new(LastWeekStart(), weekStart),
      _ => null
    };

    DateTime LastWeekStart() => weekStart.AddDays(-7);
  }

  private DateRange? GetRelativeMonthDateRange()
  {
    var today = DateTime.UtcNow;
    var monthStart = today.Day == 1 ? today : new DateTime(today.Year, today.Month, 1);
    return this.Relative switch
    {
      RelativeTo.Current => new(monthStart, today),
      RelativeTo.Previous => new(LastMonthStart(), monthStart),
      _ => null
    };

    DateTime LastMonthStart() => monthStart.AddMonths(-1);
  }

  private DateRange? GetRelativeQuarterDateRange()
  {
    var today = DateTime.UtcNow;
    var month = today.Month;
    month = month <= 3 ? 1 : (month <= 6 ? 4 : (month <= 9 ? 7 : 10));
    var qrtrStart = new DateTime(today.Year, month, 1);

    return this.Relative switch
    {
      RelativeTo.Current => new(qrtrStart, today),
      RelativeTo.Previous => new(PrevQuarterStart(), qrtrStart),
      _ => null
    };

    DateTime PrevQuarterStart() => qrtrStart.AddMonths(-3);
  }

  private DateRange? GetRelativeYearDateRange()
  {
    var today = DateTime.UtcNow;
    var yearStart = today.Month == 1 && today.Day == 1 ? today : new DateTime(today.Year, 1, 1);
    return this.Relative switch
    {
      RelativeTo.Current => new(yearStart, today),
      RelativeTo.Previous => new(LastYearStart(), yearStart),
      _ => null
    };

    DateTime LastYearStart() => yearStart.AddYears(-1);
  }
}

// by: day, week/month/year
// relative: current, prev, since
public enum DateInterval { Day, Week, Month, Quarter, Year }

public enum RelativeTo { Current, Previous, Since }
