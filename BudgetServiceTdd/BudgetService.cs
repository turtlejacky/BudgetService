using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetServiceTdd
{
	public class BudgetService
	{
		private readonly Dictionary<string, int> _budgetLookUp;
		private readonly IBudgetRepository _budgetRepository;

		public BudgetService(IBudgetRepository budgetRepository)
		{
			_budgetRepository = budgetRepository;
			_budgetLookUp = SetBudgetToLookUp();
		}

		public Dictionary<string, int> SetBudgetToLookUp()
		{
			var budgets = _budgetRepository.GetAll();
			return budgets.ToDictionary(x => x.YearMonth, x => x.Amount / GetDays(x.YearMonth));
		}

		public double TotalAmount(DateTime start, DateTime end)
		{
			return start > end ? 0 : GetYearMonthList(start, end).Sum();
		}

		private int GetDays(string yearMonth)
		{
			var year = Convert.ToInt32(yearMonth.Substring(0, 4));
			var month = Convert.ToInt32(yearMonth.Substring(4, 2));
			return DateTime.DaysInMonth(year, month);
		}

		// primitive obsession, data clump, duplication
		private IEnumerable<int> GetYearMonthList(DateTime startDate, DateTime endDate)
		{
			var period = new Period(startDate, endDate);
			var currentMonth = DateTime.ParseExact(period.StartDate.ToString("yyyyMM") + "01", "yyyyMMdd", null);
			do
			{
				var yearMonth = currentMonth.Year + TransToDateFormat(currentMonth.Month);
				var dailyAmount = _budgetLookUp.ContainsKey(yearMonth) ? _budgetLookUp[yearMonth] : 0;
				var intervalDays = period.OverlappingDays(currentMonth);
				yield return intervalDays * dailyAmount;

				currentMonth = currentMonth.AddMonths(1);
			} while (currentMonth <= period.EndDate);
		}

		private string TransToDateFormat(int i)
		{
			return i < 10 ? "0" + i : i.ToString();
		}
	}
}