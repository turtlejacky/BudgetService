using System;

namespace BudgetServiceTdd
{
	public class Period
	{
		public Period(DateTime startDate, DateTime endDate)
		{
			StartDate = startDate;
			EndDate = endDate;
		}

		public DateTime StartDate { get; private set; }
		public DateTime EndDate { get; private set; }

		public int OverlappingDays(DateTime queryMonth)
		{
			if (InvalidDate())
			{
				return 0;
			}
			var intervalDays = 0;
			if (queryMonth.Month == EndDate.Month && queryMonth.Year == EndDate.Year)
			{
				intervalDays = EndDate.Day;
			}
			else if (queryMonth.Month == StartDate.Month && queryMonth.Year == StartDate.Year)
			{
				intervalDays = (DateTime.DaysInMonth(queryMonth.Year, queryMonth.Month) - StartDate.Day + 1);
			}
			else if (queryMonth > StartDate && queryMonth < EndDate)
			{
				intervalDays = DateTime.DaysInMonth(queryMonth.Year, queryMonth.Month);
			}

			return intervalDays;
		}

		private bool InvalidDate()
		{
			return StartDate > EndDate;
		}
	}
}