using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wallets_API.DBClasses;
using Wallets_API.DTO;
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
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if(user != null)
            {
                var expenses = await _expenseRepository.ShowAllExpenses(user.WalletID);
                if (expenses != null && expenses.Count() > 0)
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
                else
                    return BadRequest("There is no expenses");
            }
            return BadRequest("Could not get expenses");
        }

        [HttpPost("new")]
        public async Task<IActionResult> CreateExpense(string userId, Expense newExpense)
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

    }
}