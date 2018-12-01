using System;

namespace BudgetServiceTdd
{
	public class Budget
	{
		public string YearMonth { get; set; }
		public int Amount { get; set; }

		public int DaysInMonth => DateTime.DaysInMonth(YearMonthInDateTime.Year, YearMonthInDateTime.Month);

		public DateTime YearMonthInDateTime => DateTime.ParseExact(YearMonth, "yyyyMM", null);

		public int DailyAmount => Amount / DaysInMonth;
	}
}