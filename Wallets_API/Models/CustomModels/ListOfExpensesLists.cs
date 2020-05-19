using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.DBClasses;
using Wallets_API.DTO;

namespace Wallets_API.Models.CustomModels
{
    public class ListOfExpensesLists
    {
        public List<Expense> food { get; set; }
        public List<Expense> housekeeping { get; set; }
        public List<Expense> entertainment { get; set; }
        public List<Expense> clothes { get; set; }
        public List<Expense> other { get; set; }
        public string WalletTitle { get; set; }
        public int? MonthlyLimit { get; set; }
    }
}
