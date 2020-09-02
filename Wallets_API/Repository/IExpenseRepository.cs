using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.DBClasses;
using Wallets_API.DTO;
using Wallets_API.Models.CustomModels;

namespace Wallets_API.Repository
{
    public interface IExpenseRepository
    {
        Task<List<ExpensesWithCategoryData>> ShowExpenses(int walletId);
        Task<List<ExpenseDTO>> ShowDailyExpenses(int walletId, DateTime date);
        Task<PreviousExpenses> ShowPreviousExpenses(int walletId, int month);
        Task<WalletToReturnDTO> GetWalletData(int walletId);
        Task<List<ExpenseDTO>> ShowCategoryExpenses(int walletId, int categoryId, DateTime[] date);
        Task<Expense> CreateNewExpense(Expense newExpense);
        Task<List<CategoriesAndExpensesDTO>> CreateBarExpensesData(int walletId, int month);
        Task<DetailedWalletStatisticsDTO> DetailedWalletStatistics(int walletId);
        Task<DetailedCategoryStatisticsDTO> DetailedCategoryStatistics(int walletId, int categoryId, string userId);
        Task<DetailedUserStatisticsDTO> DetailedUserStatistics(int walletId, string userId, int month);
        Task<List<ExpenseDTO>> ShowUserExpenses(int walletId, string userId, int month);
        Task<ResponseData> DeleteExpense(string userId, int expenseId);
        Task<Expense> EditExpense(string userId, ExpenseDTO expenseToEdit);

        
    }
}
