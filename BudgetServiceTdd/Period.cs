using System;

namespace BudgetServiceTdd
{
	public class Period
	{
		private readonly MonthHandler _monthHandler;

		public Period(DateTime startDate, DateTime endDate)
		{
			StartDate = startDate;
			EndDate = endDate;
			_monthHandler = InitialMonthHandler();
		}

		public DateTime StartDate { get; private set; }
		public DateTime EndDate { get; private set; }

		public int OverlappingDays(DateTime queryMonth)
		{
			if (InvalidDate())
			{
				return 0;
			}

			return _monthHandler.Handle(this, queryMonth);
		}

		private MonthHandler InitialMonthHandler()
		{
			var lastMonthHandler = new LastMonthHandler();
			var firstMonthHandler = new FirstMonthHandler();
			var betweenMonthHandler = new BetweenMonthHandler();

			lastMonthHandler.Successor = firstMonthHandler;
			firstMonthHandler.Successor = betweenMonthHandler;

			return lastMonthHandler;
		}

		private bool InvalidDate()
		{
			return StartDate > EndDate;
		}
	}
}