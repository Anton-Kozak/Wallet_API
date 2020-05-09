using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.DBClasses;
using Wallets_API.DTO;

namespace Wallets_API.Repository
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> ShowAllExpenses(int walletId);
        Task<Expense> CreateNewExpense(Expense newExpense);
        Task<BarExpensesDTO> CreateBarExpensesData(int walletId);
    }
}
