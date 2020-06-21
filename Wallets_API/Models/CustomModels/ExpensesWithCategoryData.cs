using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.DBClasses;
using Wallets_API.DTO;

namespace Wallets_API.Models.CustomModels
{
    public class ExpensesWithCategoryData
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ExpenseDTO> Expenses { get; set; }
    }
}
