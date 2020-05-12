using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.Models.CustomModels;

namespace Wallets_API.DTO
{
    public class DetailedCategoryStatisticsDTO
    {
        public double LargestExpense { get; set; }
        public double CurrentMonthLargestExpense { get; set; }
        public UserStatistics MostSpentUser { get; set; }
        public UserStatistics MostUsedUser { get; set; }

        public CategoryComparisonData BarCompareExpensesWithLastMonth { get; set; }
        public List<UserStatistics> TopFiveUsers { get; set; }
        public List<LastMonthData> LastSixMonths { get; set; }
        public double SpentThisMonth { get; set; }
        public double SpentAll { get; set; }

    }
}
