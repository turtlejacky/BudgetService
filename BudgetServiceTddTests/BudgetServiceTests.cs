using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BudgetServiceTdd.Tests
{
    [TestClass()]
    public class BudgetServiceTests
    {
        private readonly FakeRepository _budgetRepository = new FakeRepository();
        private readonly BudgetService _budgetService;
        private DateTime _endDate;
        private DateTime _startDate;

        public BudgetServiceTests()
        {
            _budgetService = new BudgetService(_budgetRepository);
        }

        [TestMethod]
        public void Cross_two_months()
        {
            _startDate = new DateTime(2018, 4, 30);
            _endDate = new DateTime(2018, 5, 2);
            TheBudgetShouldBe(12);
        }

        [TestMethod]
        public void Only_one_month_sum_of_2days()
        {
            _startDate = new DateTime(2018, 4, 1);
            _endDate = new DateTime(2018, 4, 2);
            TheBudgetShouldBe(20);
        }

        [TestMethod]
        public void StartDate_bigger_than_EndDate()
        {
            _startDate = new DateTime(2018, 4, 30);
            _endDate = new DateTime(2018, 4, 1);
            TheBudgetShouldBe(0);
        }

        [TestMethod]
        public void The_date_never_be_set()
        {
            _startDate = new DateTime(2018, 9, 1);
            _endDate = new DateTime(2018, 9, 2);
            TheBudgetShouldBe(0);
        }

        [TestMethod]
        public void Trans_to_dictionary_test()
        {
            var budgetToLookUp = _budgetService.SetBudgetToLookUp();
            var expected = new Dictionary<string, int>()
            {
                { "201804",10},
                { "201805",1}
            };
            expected.ToExpectedObject().ShouldEqual(budgetToLookUp);
        }

        private void TheBudgetShouldBe(int expected)
        {
            Assert.AreEqual(expected, _budgetService.TotalAmount(_startDate, _endDate));
        }
    }

    internal class FakeRepository : IBudgetRepository
    {
        public List<Budget> GetAll()
        {
            return new List<Budget>()
            {
                new Budget
                {
                    YearMonth = "201804",
                    Amount = 300
                },
                new Budget
                {
                    YearMonth = "201805",
                    Amount = 31
                }
            };
        }
    }
}