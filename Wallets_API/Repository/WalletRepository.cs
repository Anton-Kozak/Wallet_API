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

        public async Task<bool> AddCategoriesToWallet(int walletId)
        {
            WalletsCategories walletsCategories = new WalletsCategories
            {
                WalletId = walletId,
                CategoryId = 1
            };
            WalletsCategories walletsCategories2 = new WalletsCategories
            {
                WalletId = walletId,
                CategoryId = 6
            };
            WalletsCategories walletsCategories3 = new WalletsCategories
            {
                WalletId = walletId,
                CategoryId = 7
            };
            _context.WalletsCategories.Add(walletsCategories);
            _context.WalletsCategories.Add(walletsCategories2);
            _context.WalletsCategories.Add(walletsCategories3);
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
                                 CategoryId = wc.CategoryId,
                                 CategoryName = c.Title
                             }).ToListAsync();
            return cat;
        }
    }
}
