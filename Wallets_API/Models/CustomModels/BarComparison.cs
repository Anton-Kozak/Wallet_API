using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.DTO;

namespace Wallets_API.Models.CustomModels
{
    public class BarComparison
    {
        public BarExpensesDTO CurrentMonthData { get; set; }
        public BarExpensesDTO LastMonthData { get; set; }
    }
}
