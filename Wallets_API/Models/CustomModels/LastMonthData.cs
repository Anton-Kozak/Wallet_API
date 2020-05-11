using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wallets_API.Models.CustomModels
{
    public class LastMonthData
    {
        public string Month { get; set; }
        public double ExpenseSum { get; set; }
    }
}
