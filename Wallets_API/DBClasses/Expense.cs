using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wallets_API.DBClasses
{
    public class Expense
    {
        public int Id { get; set; }
        public int ExpenseCategoryId { get; set; }
        public string ExpenseTitle { get; set; }
        public string ExpenseDescription { get; set; }
        public DateTime CreationDate { get; set; }
        public double MoneySpent { get; set; }
        public string ExpenseUserId { get; set; }
        public int FamilyWalletId { get; set; }
    }
}
