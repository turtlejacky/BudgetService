using System;

namespace BudgetServiceTdd
{
	internal class BetweenMonthHandler : MonthHandler
	{
		public override int Handle(Period period, DateTime queryMonth)
		{
			if (queryMonth > period.StartDate && queryMonth < period.EndDate)
			{
				return DateTime.DaysInMonth(queryMonth.Year, queryMonth.Month);
			}
			else
			{
				return 0;
			}
		}
	}
}