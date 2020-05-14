﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.DBClasses;
using Wallets_API.DTO;

namespace Wallets_API.Repository
{
    public interface IExpenseRepository
    {
        Task<List<Expense>> ShowAllExpenses(int walletId);
        Task<List<ExpenseDTO>> ShowCategoryExpenses(int walletId, int categoryId);
        Task<Expense> CreateNewExpense(Expense newExpense);
        Task<BarExpensesDTO> CreateBarExpensesData(int walletId);
        Task<DetailedWalletStatisticsDTO> DetailedWalletStatistics(int walletId);
        Task<DetailedCategoryStatisticsDTO> DetailedCategoryStatistics(int walletId, int categoryId, string userId);
        Task<DetailedUserStatisticsDTO> DetailedUserStatistics(int walletId, string userId);
        Task<List<ExpenseDTO>> ShowUserExpenses(int walletId, string userId);
    }
}
