using System;

namespace BudgetServiceTdd.Handlers
{
	public abstract class MonthHandler
	{
		public abstract int Handle(Period period, DateTime queryMonth);
		public MonthHandler Successor { get; set; }
	}
}