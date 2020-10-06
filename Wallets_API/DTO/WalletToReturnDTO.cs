using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wallets_API.DTO
{
    public class WalletToReturnDTO
    {
        public string Title { get; set; }
        public int MonthlyLimit { get; set; }
        public string Currency { get; set; }
        public double MonthlyExpenses { get; set; }
        public List<int> WalletCategories { get; set; }
    }
}
