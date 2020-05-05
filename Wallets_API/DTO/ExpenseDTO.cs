using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wallets_API.DTO
{
    public class ExpenseDTO
    {
        public string UserName { get; set; }
        public int ExpenseId { get; set; }
        public string ExpenseTitle { get; set; }
        public DateTime CreationDate { get; set; }
        public double MoneySpend { get; set; }
        public string CategoryTitle { get; set; }
    }
}
