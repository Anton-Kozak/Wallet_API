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
using Wallets_API.Models;
using Wallets_API.Repository;

namespace Wallets_API.Controllers
{
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
                    return Ok(expenses);
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
                if (await _expenseRepository.CreateNewExpense(newExpense))
                    return Ok("Expense has been successfully created");
                return BadRequest("Something went wrong");
            }
            return BadRequest("Could not create an expense");
        }

    }
}