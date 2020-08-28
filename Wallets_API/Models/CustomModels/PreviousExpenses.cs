using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.DTO;

namespace Wallets_API.Models.CustomModels
{
    public class PreviousExpenses
    {
        public List<ExpensesWithCategoryData> PreviousMonthExpenses { get; set; }
        public List<UserStatistics> TopFiveUsers { get; set; }
        public List<CategoriesAndExpensesDTO> PreviousExpensesBars { get; set; }
    }
}
