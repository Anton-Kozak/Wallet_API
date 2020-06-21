using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.Data;
using Wallets_API.DBClasses;
using Wallets_API.DTO;
using Wallets_API.Models;
using Wallets_API.Models.CustomModels;

namespace Wallets_API.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ApplicationDbContext _context;

        public WalletRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<bool> CreateWallet(Wallet wallet, ApplicationUser user)
        {
            wallet.UserNumber = 1;
            _context.Wallets.Add(wallet);

            if (await _context.SaveChangesAsync() > 0)
            {
                user.WalletID = wallet.Id;
                user.DateJoined = DateTime.Now;
                user.IsWalletAdmin = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public async Task<Wallet> GetCurrentWallet(int walletId)
        {
            var walletToEdit = await _context.Wallets.Where(w => w.Id == walletId).FirstOrDefaultAsync();
            if (walletToEdit != null)
            {
                return walletToEdit;
            }
            return null;
        }

        public async Task<ResponseData> EditWallet(int walletId, WalletToReturnDTO walletToEdit)
        {
            ResponseData responseData = new ResponseData
            {
                isSuccessful = false,
                Message = "No wallet has been found",
            };
            var wallet = await _context.Wallets.Where(w => w.Id == walletId).FirstOrDefaultAsync();
            if (wallet != null)
            {
                var monthlyExpenses = await GetMonthlyExpenses(walletId);

                if (walletToEdit.MonthlyLimit <= monthlyExpenses)
                {
                    responseData.Message = "You cannot set limit that exceeds your expenses!";
                    return responseData;
                }
                wallet.Title = walletToEdit.Title;
                wallet.MonthlyLimit = walletToEdit.MonthlyLimit;
                _context.Wallets.Update(wallet);
                if (await _context.SaveChangesAsync() > 0)
                {
                    responseData.isSuccessful = true;
                    responseData.Message = "Edit was successful";
                    return responseData;
                }
                responseData.Message = "Edit was not successful";
                return responseData;
            }
            return responseData;

        }

        private async Task<double> GetMonthlyExpenses(int walletId)
        {
            var today = DateTime.Today;
            var currentMonth = new DateTime(today.Year, today.Month, 1);
            var endOfCurrentMonth = currentMonth.AddMonths(1).AddMilliseconds(-1);
            return await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.CreationDate >= currentMonth && e.CreationDate <= endOfCurrentMonth).SumAsync(m => m.MoneySpent);
        }

        public async Task<bool> AddCategoriesToWallet(int walletId, int[] categories)
        {
            foreach (var categoryId in categories)
            {
                if (!await _context.WalletsCategories.Where(wc => wc.CategoryId == categoryId && wc.WalletId == walletId).AnyAsync() && await _context.ExpenseCategories.Where(ec=> ec.Id == categoryId).AnyAsync())
                {
                    WalletsCategories newCategory = new WalletsCategories
                    {
                        WalletId = walletId,
                        CategoryId = categoryId
                    };
                    _context.WalletsCategories.Add(newCategory);
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<WalletCategoriesToReturnDTO>> GetCategories(int walletId)
        {
            var cat = await (from wc in _context.WalletsCategories
                             join c in _context.ExpenseCategories
                             on wc.CategoryId equals c.Id
                             where wc.WalletId == walletId
                             select new WalletCategoriesToReturnDTO
                             {
                                 Id = wc.CategoryId,
                                 Title = c.Title
                             }).ToListAsync();
            return cat;
        }
    }
}
