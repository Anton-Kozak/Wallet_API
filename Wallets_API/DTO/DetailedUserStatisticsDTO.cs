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
        public double AverageDailyExpense { get; set; }
        public BarExpensesDTO BarExpenses { get; set; }
        public BarComparison BarCompareExpensesWithLastMonth { get; set; }
        public List<UserStatistics> TopFiveUsers { get; set; }
        public List<LastMonthData> LastSixMonths { get; set; }
        public string[] WalletUsers { get; set; }
        public string WalletAdmin { get; set; }
        public double AmountOfMoneySpent { get; set; }
    }
}
