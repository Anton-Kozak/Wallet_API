using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.Models.CustomModels;

namespace Wallets_API.DTO
{
    public class DetailedUserStatisticsDTO
    {
        public string MostSpentCategory { get; set; }
        public string MostUsedCategory { get; set; }
        public double LargestExpense { get; set; }
        public double AverageDailyExpense { get; set; }
        public List<CategoriesAndExpensesDTO> BarExpenses { get; set; }
        public double AmountOfMoneySpent { get; set; }
        public MonthsComparisonData MonthCompareData { get; set; }
        public List<LastMonthData> LastSixMonths { get; set; }
    }
}
