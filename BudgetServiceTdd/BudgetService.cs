using System;
using System.Linq;

namespace BudgetServiceTdd
{
	public class BudgetService
	{
		private readonly IBudgetRepository _budgetRepository;

		public BudgetService(IBudgetRepository budgetRepository)
		{
			_budgetRepository = budgetRepository;
		}

		public double TotalAmount(DateTime start, DateTime end)
		{
			var period = new Period(start, end);
			return _budgetRepository.GetAll() .Select(b => b.DailyAmount * period.OverlappingDays(b.YearMonthInDateTime)).Sum();
		}

	}
}