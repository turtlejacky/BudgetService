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
			for (var year = startDate.Year; year <= endDate.Year; year++)
			{
				var startMonth = year == startDate.Year ? startDate.Month : 1;
				var endMonth = year == endDate.Year ? endDate.Month : 12;
				for (var month = startMonth; month <= endMonth; month++)
				{
					var yearMonth = year + TransToDateFormat(month);
					var dailyAmount = _budgetLookUp.ContainsKey(yearMonth) ? _budgetLookUp[yearMonth] : 0;
					int intervalDays;
					if (IsLastMonth(endDate, month, year))
					{
						intervalDays = endDate.Day;
					}
					else if (IsFirstMonth(startDate, month, year))
					{
						intervalDays = (DateTime.DaysInMonth(year, month) - startDate.Day + 1);
					}
					else
					{
						intervalDays = DateTime.DaysInMonth(year, month);
					}
					yield return intervalDays * dailyAmount;
				}
			}
		}

		private bool IsFirstMonth(DateTime start, int month, int year)
		{
			return month == start.Month && year == start.Year;
		}

		private bool IsLastMonth(DateTime endDate, int month, int year)
		{
			return month == endDate.Month && year == endDate.Year;
		}

		private string TransToDateFormat(int i)
		{
			return i < 10 ? "0" + i : i.ToString();
		}
	}
}