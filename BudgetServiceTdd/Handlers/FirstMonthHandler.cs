using System;

namespace BudgetServiceTdd.Handlers
{
	public class FirstMonthHandler : MonthHandler
	{
		public override int Handle(Period period, DateTime queryMonth)
		{
			if (queryMonth.Month == period.StartDate.Month && queryMonth.Year == period.StartDate.Year)
			{
				return (DateTime.DaysInMonth(queryMonth.Year, queryMonth.Month) - period.StartDate.Day + 1);
			}
			else
			{
				return this.Successor.Handle(period, queryMonth);
			}
		}
	}
}