using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
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
    //[Authorize(Policy = "Member")]
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
                            clothes = new List<Expense>(),
                            other = new List<Expense>(),
                        };
                        foreach (var expense in expenses)
                        {
                            switch (expense.ExpenseCategoryId)
                            {
                                case 1:
                                    list.food.Add(expense);
                                    break;
                                case 2:
                                    list.housekeeping.Add(expense);
                                    break;
                                case 3:
                                    list.clothes.Add(expense);
                                    break;
                                case 4:
                                    list.entertainment.Add(expense);
                                    break;
                                case 5:
                                    list.other.Add(expense);
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

        [HttpGet("getNameAndLimit")]
        public async Task<IActionResult> GetWalletTitleAndLimit(string userId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user != null)
                {
                    var otherData = await _expenseRepository.GetWalletData(user.WalletID);
                    if (otherData != null)
                    {
                        WalletToReturnDTO walletData = new WalletToReturnDTO();
                        walletData.Title = otherData.Title;
                        walletData.MonthlyLimit = otherData.MonthlyLimit;
                        return Ok(walletData);
                    }
                }
                return BadRequest("Could not retreive wallet's data");
            }
            return Unauthorized();
        }


        [HttpGet("getCategoryExpenses/{categoryId}")]
        public async Task<IActionResult> GetCategoryExpenses(string userId, int categoryId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                if (categoryId != 0)
                {
                    var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                    if (user != null)
                    {
                        var result = await _expenseRepository.ShowCategoryExpenses(user.WalletID, categoryId);
                        if (result != null)
                        {
                            return Ok(result);
                        }
                    }
                    return BadRequest("User has not been found");
                }
                return BadRequest("There is no category");

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

        [HttpDelete("expenseDelete/{expenseId}")]
        public async Task<IActionResult> DeleteExpense(string userId, int expenseId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (user != null)
                {
                    var result = await _expenseRepository.DeleteExpense(userId, expenseId);
                    if (result.isSuccessful)
                        return Ok(result.Message);
                    return BadRequest(result.Message);
                }
                return BadRequest("No user hsa been found");
            }
            return Unauthorized();
        }

        [HttpPut("expenseEdit/{expenseId}")]
        public async Task<IActionResult> DeleteExpense(string userId, int expenseId, ExpenseDTO expense)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (user != null)
                {
                    var result = await _expenseRepository.EditExpense(userId, expense);
                    if (result.isSuccessful)
                        return Ok(result.Message);
                    return BadRequest(result.Message);
                }
                return BadRequest("No user has been found");
            }
            return Unauthorized();
        }

        [HttpGet("barExpenses")]
        public async Task<IActionResult> ShowBarExpensesData(string userId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user != null && user.WalletID != 0)
                {
                    var result = await _expenseRepository.CreateBarExpensesData(user.WalletID);
                    return Ok(result);
                }
                return BadRequest();
            }
            return Unauthorized();
        }

        [HttpGet("detailedStatistics")]
        public async Task<IActionResult> GetDetailedWalletStatistics(string userId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user != null && user.WalletID != 0)
                {
                    var result = await _expenseRepository.DetailedWalletStatistics(user.WalletID);
                    return Ok(result);
                }
            }
            return Unauthorized();
        }

        [HttpGet("detailedCategoryStatistics/{categoryId}")]
        public async Task<IActionResult> GetDetailedCategoryStatistics(string userId, int categoryId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                if (categoryId <= 0 || categoryId > 5)
                    return BadRequest("Category is not found");
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user != null && user.WalletID != 0)
                {
                    var result = await _expenseRepository.DetailedCategoryStatistics(user.WalletID, categoryId, userId);
                    return Ok(result);
                }
                return BadRequest("User or wallet is not found");
            }
            return Unauthorized();
        }

        [HttpGet("detailedUserStatistics/{userToShowId}")]
        public async Task<IActionResult> GetDetailedUserStatistics(string userId, string userToShowId)
        {
            //if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userToShowId);
                var currentUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user != null && user.WalletID != 0 && user.WalletID == currentUser.WalletID)
                {
                    var result = await _expenseRepository.DetailedUserStatistics(user.WalletID, userToShowId);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    return BadRequest("Something went wrong");
                }
                return BadRequest("User or wallet is not found");
            }
            //return Unauthorized();
        }

        [HttpGet("getUserExpenses/{userToDisplayId}")]
        public async Task<IActionResult> GetUserExpenses(string userId, string userToDisplayId)
        {
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userToDisplayId);
                var currentUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user != null && user.WalletID == currentUser.WalletID)
                {
                    var result = await _expenseRepository.ShowUserExpenses(user.WalletID, userToDisplayId);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                }
                return BadRequest("User has not been found");
            }
        }

    }
}