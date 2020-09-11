using System;
using System.Collections.Generic;
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
        Task<PreviousExpenses> ShowPreviousExpenses(int walletId, DateTime date);
        Task<SpecifiedMonthsData> ShowSpecifiedMonthsData(int walletId, DateTime firstMonth, DateTime secondMonth);
        Task<WalletToReturnDTO> GetWalletData(int walletId);
        Task<List<ExpenseDTO>> ShowCategoryExpenses(int walletId, int categoryId, DateTime[] date);
        Task<Expense> CreateNewExpense(Expense newExpense);
        Task<List<CategoriesAndExpensesDTO>> CreateBarExpensesData(int walletId, DateTime date);
        Task<DetailedWalletStatisticsDTO> DetailedWalletStatistics(int walletId, DateTime date);
        Task<DetailedCategoryStatisticsDTO> DetailedCategoryStatistics(int walletId, int categoryId, string userId, DateTime date);
        Task<DetailedUserStatisticsDTO> DetailedUserStatistics(int walletId, string userId, DateTime date);
        Task<List<ExpenseDTO>> ShowUserExpenses(int walletId, string userId, DateTime d);
        Task<ResponseData> DeleteExpense(string userId, int expenseId);
        Task<Expense> EditExpense(string userId, ExpenseDTO expenseToEdit);


    }
}
