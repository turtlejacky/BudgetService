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

		public int OverlappingDays(DateTime currentMonth)
		{
			if (InvalidDate())
			{
				return 0;
			}
			var intervalDays = 0;
			if (IsLastMonth(EndDate, currentMonth))
			{
				intervalDays = EndDate.Day;
			}
			else if (IsFirstMonth(StartDate, currentMonth))
			{
				intervalDays = (DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month) - StartDate.Day + 1);
			}
			else if (currentMonth > StartDate && currentMonth < EndDate)
			{
				intervalDays = DateTime.DaysInMonth(currentMonth.Year, currentMonth.Month);
			}

			return intervalDays;
		}

		private bool InvalidDate()
		{
			return StartDate > EndDate;
		}

		private static bool IsFirstMonth(DateTime startDate, DateTime currentMonth)
		{
			return currentMonth.Month == startDate.Month && currentMonth.Year == startDate.Year;
		}

		private static bool IsLastMonth(DateTime endDate, DateTime currentMonth)
		{
			return currentMonth.Month == endDate.Month && currentMonth.Year == endDate.Year;
		}
	}
}