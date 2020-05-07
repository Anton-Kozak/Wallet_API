using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Wallets_API.DBClasses;
using Wallets_API.Models;
using Wallets_API.Models.CustomModels;
using Wallets_API.Repository;

namespace Wallets_API.Controllers
{
    //TODO: сделать проверку на то, является ли пользователь тем кем нужно
    [Route("api/[controller]/{userId}/")]
    [ApiController]
    [Authorize]
    public class ExpenseController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseController(UserManager<ApplicationUser> userManager, IExpenseRepository expenseRepository)
        {
            _userManager = userManager;
            _expenseRepository = expenseRepository;
        }


        [HttpGet]
        public async Task<IActionResult> ShowAllExpenses(string userId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (user != null)
                {
                    var expenses = await _expenseRepository.ShowAllExpenses(user.WalletID);
                    if (expenses != null)
                    {
                        ListOfExpensesLists list = new ListOfExpensesLists()
                        {
                            food = new List<Expense>(),
                            entertainment = new List<Expense>(),
                            housekeeping = new List<Expense>(),
                        };
                        foreach (var expense in expenses)
                        {
                            switch (expense.ExpenseCategoryId)
                            {
                                case 1:
                                    list.housekeeping.Add(expense);
                                    break;
                                case 2:
                                    list.entertainment.Add(expense);
                                    break;
                                case 3:
                                    list.food.Add(expense);
                                    break;
                            }
                        }
                        return Ok(list);
                    }
                }
                return BadRequest(null);
            }
            return Unauthorized();
        }

        [HttpPost("new")]
        public async Task<IActionResult> CreateExpense(string userId, Expense newExpense)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (user != null)
                {
                    newExpense.ExpenseUserId = userId;
                    newExpense.FamilyWalletId = user.WalletID;
                    if (await _expenseRepository.CreateNewExpense(newExpense) != null)
                        return Ok(newExpense);
                    return BadRequest("Something went wrong");
                }
                return BadRequest("Could not create an expense");
            }
            return Unauthorized();
        }

    }
}