using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.DBClasses;

namespace Wallets_API.DTO
{
    public class ExpenseWithMessageDTO
    {
        public Expense Expense { get; set; }
        public string Message { get; set; }
    }
}
