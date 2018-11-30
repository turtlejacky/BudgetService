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

        private IEnumerable<int> GetYearMonthList(DateTime startDate, DateTime endDate)
        {
            for (var year = startDate.Year; year <= endDate.Year; year++)
            {
                for (var month = startDate.Month; month <= endDate.Month; month++)
                {
                    var yearMonth = year + TransToDateFormat(month);
                    var budget = _budgetLookUp.ContainsKey(yearMonth) ? _budgetLookUp[yearMonth] : 0;
                    if (IsLsatMonth(endDate, month, year))
                    {
                        yield return endDate.Day * budget;
                    }
                    else if (IsFirstMonth(startDate, month, year))
                    {
                        yield return (DateTime.DaysInMonth(year, month) - startDate.Day + 1) * budget;
                    }
                    else
                    {
                        yield return DateTime.DaysInMonth(year, month) * budget;
                    }
                }
            }
        }

        private bool IsFirstMonth(DateTime start, int month, int year)
        {
            return month == start.Month && year == start.Year;
        }

        private bool IsLsatMonth(DateTime endDate, int month, int year)
        {
            return month == endDate.Month && year == endDate.Year;
        }

        private string TransToDateFormat(int i)
        {
            return i < 10 ? "0" + i : i.ToString();
        }
    }
}