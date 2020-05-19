using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wallets_API.DTO
{
    public class ExpenseDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string ExpenseTitle { get; set; }
        public string ExpenseDescription { get; set; }
        public DateTime CreationDate { get; set; }
        public double MoneySpent { get; set; }
    }
}
