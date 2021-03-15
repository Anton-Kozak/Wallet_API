using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public AdminRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<ExpenseForAdminDTO>> GetAllExpenses(int walletId)
        {
            var expenses = await (from e in _context.Expenses
                                  join u in _context.Users
                                  on e.ExpenseUserId equals u.Id
                                  join c in _context.ExpenseCategories
                                  on e.ExpenseCategoryId equals c.Id
                                  where e.FamilyWalletId == walletId
                                  select new ExpenseForAdminDTO
                                  {
                                      Id = e.Id,
                                      UserName = u.UserName,
                                      ExpenseTitle = e.ExpenseTitle,
                                      ExpenseDescription = e.ExpenseDescription,
                                      CreationDate = e.CreationDate,
                                      MoneySpent = e.MoneySpent,
                                      ExpenseCategory = c.Title
                                  }).ToListAsync();

            var reversedExpenses = expenses.Reverse<ExpenseForAdminDTO>().OrderByDescending(e => e.CreationDate).ToList();
            return reversedExpenses;
        }

        public async Task<List<UserToReturnToAdmin>> GetUserData(int walletId)
        {
            //TODO: добавить роли
            var users = await (from u in _context.Users
                               where u.WalletID == walletId
                               select new UserToReturnToAdmin
                               {
                                   Id = u.Id,
                                   Address = u.Address,
                                   Age = u.Age,
                                   DateJoined = u.DateJoined,
                                   Username = u.UserName,
                                   User = u,
                               }).ToListAsync();
            foreach (var user in users)
            {
                user.UserRoles = await _userManager.GetRolesAsync(user.User);
            }
            return users;
        }

        public async Task<bool> RemoveUserAsync(ApplicationUser userToRemove, int walletId)
        {
            userToRemove.WalletID = 0;
            var wallet = await _context.Wallets.Where(w => w.Id == walletId).FirstOrDefaultAsync();
            wallet.UserNumber--;
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<ResponseData> DeleteExpense(int expenseId)
        {
            ResponseData data = new ResponseData
            {
                isSuccessful = false,
                Message = "",
            };
            var expenseToDelete = await _context.Expenses.Where(e => e.Id == expenseId).FirstOrDefaultAsync();

            if (expenseToDelete != null)
            {
                _context.Expenses.Remove(expenseToDelete);
                await _context.SaveChangesAsync();
                data.isSuccessful = true;
                data.Message = "Expense has been successfully deleted";
                return data;
            }
            data.Message = "Expense has not been found";
            return data;
        }

        public async Task<Expense> EditExpense(ExpenseDTO expenseToEdit)
        {
            var expToEdit = await _context.Expenses.Where(e => e.Id == expenseToEdit.Id).FirstOrDefaultAsync();

            if (expToEdit != null)
            {
                expToEdit.ExpenseTitle = expenseToEdit.ExpenseTitle;
                expToEdit.ExpenseDescription = expenseToEdit.ExpenseDescription;
                expToEdit.MoneySpent = expenseToEdit.MoneySpent;
                expToEdit.CreationDate = expenseToEdit.CreationDate;
                _context.Expenses.Update(expToEdit);
                await _context.SaveChangesAsync();
                return expToEdit;
            }
            return null;
        }
    }
}
