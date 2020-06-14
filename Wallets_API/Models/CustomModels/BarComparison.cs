using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.DTO;

namespace Wallets_API.Models.CustomModels
{
    public class BarComparison
    {
        public List<CategoriesAndExpensesDTO> CurrentMonthData { get; set; }
        public List<CategoriesAndExpensesDTO> LastMonthData { get; set; }
    }
}
