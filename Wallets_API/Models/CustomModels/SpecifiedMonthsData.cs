using System.Collections.Generic;
using Wallets_API.DTO;

namespace Wallets_API.Models.CustomModels
{
    public class SpecifiedMonthsData
    {
        public string FirstMonthMostUsed { get; set; }
        public string FirstMonthMostSpent { get; set; }
        public double FirstMonthAverage { get; set; }
        public double FirstMonthTotal { get; set; }
        public double FirstLargestExpense { get; set; }
        public List<UserStatistics> FirstMonthTopFiveUsers { get; set; }
        public List<CategoriesAndExpensesDTO> FirstMonthPreviousExpensesBars { get; set; }
        public List<ExpenseDTO> FirstMonthExpenses { get; set; }

        public string SecondMonthMostUsed { get; set; }
        public string SecondMonthMostSpent { get; set; }
        public double SecondMonthAverage { get; set; }
        public double SecondMonthTotal { get; set; }
        public double SecondLargestExpense { get; set; }
        public List<UserStatistics> SecondMonthTopFiveUsers { get; set; }
        public List<CategoriesAndExpensesDTO> SecondMonthPreviousExpensesBars { get; set; }
        public List<ExpenseDTO> SecondMonthExpenses { get; set; }
    }
}
