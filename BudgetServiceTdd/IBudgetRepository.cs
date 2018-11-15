using System.Collections.Generic;

namespace BudgetServiceTdd
{
	public interface IBudgetRepository
	{
		List<Budget> GetAll();
	}
}