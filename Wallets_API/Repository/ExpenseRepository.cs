using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.Data;
using Wallets_API.DBClasses;
using Wallets_API.DTO;

namespace Wallets_API.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _context;
        public ExpenseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BarExpensesDTO> CreateBarExpensesData(int walletId)
        {
            //TODO: проверить что будет если везде будет 0
            var houseData = await _context.Expenses.Where(e => e.ExpenseCategoryId == 1).SumAsync(e => e.MoneySpent);
            var enterData = await _context.Expenses.Where(e => e.ExpenseCategoryId == 2).SumAsync(e => e.MoneySpent);
            var foodData = await _context.Expenses.Where(e => e.ExpenseCategoryId == 3).SumAsync(e => e.MoneySpent);
            BarExpensesDTO barExpenses = new BarExpensesDTO
            {
                HouseExpenses = houseData,
                FoodExpenses = foodData,
                EntertainmentExpenses = enterData
            };
            return barExpenses;
        }

        public async Task<Expense> CreateNewExpense(Expense newExpense)
        {
            _context.Expenses.Add(newExpense);
            if (await _context.SaveChangesAsync() > 0)
                return newExpense;
            return null;
        }

        public async Task<IEnumerable<Expense>> ShowAllExpenses(int walletId)
        {
            return await _context.Expenses.Where(e => e.FamilyWalletId == walletId).ToListAsync();
        }
    }
}
