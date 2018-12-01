using System;
using BudgetServiceTdd;

public class LastMonthHandler : MonthHandler
{
	public override int Handle(Period period, DateTime queryMonth)
	{
		if (queryMonth.Month == period.EndDate.Month && queryMonth.Year == period.EndDate.Year)
		{
			return period.EndDate.Day;
		}
		else
		{
			return this.Successor.Handle(period, queryMonth);
		}
	}
}