using System;

namespace BudgetServiceTdd
{
	public class Budget
	{
		public string YearMonth { get; set; }
		public int Amount { get; set; }

		public int DaysInMonth
		{
			get
			{
				var yearMonthInDateTime = DateTime.ParseExact(YearMonth, "yyyyMM", null);
				return DateTime.DaysInMonth(yearMonthInDateTime.Year, yearMonthInDateTime.Month);
			}
		}
	}
}