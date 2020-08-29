using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Wallets_API.Data;
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
    [AllowAnonymous]
    //[Authorize(Policy = "Member")]
    public class ExpenseController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IExpenseRepository _expenseRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ExpenseController(UserManager<ApplicationUser> userManager, IExpenseRepository expenseRepository,
            INotificationRepository notificationRepository, ApplicationDbContext context, IMapper mapper)
        {
            _userManager = userManager;
            _expenseRepository = expenseRepository;
            _notificationRepository = notificationRepository;
            _context = context;
            _mapper = mapper;
        }

        private string GetCategoryName(int categoryId)
        {
            var cat = (from wc in _context.WalletsCategories
                       join c in _context.ExpenseCategories
                       on wc.CategoryId equals c.Id
                       select c.Title).FirstOrDefault();
            return cat;
        }

        [HttpGet]
        public async Task<IActionResult> ShowCurrentExpenses(string userId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user != null)
                {
                    var expenses = await _expenseRepository.ShowExpenses(user.WalletID);
                    if (expenses != null)
                    {
                        return Ok(expenses);
                    }
                }
                return BadRequest(null);
            }
            return Unauthorized();
        }

        [HttpGet("previousExpenses/{month}")]
        public async Task<IActionResult> ShowPreviousExpenses(string userId, int month)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user != null)
                {
                    var expenses = await _expenseRepository.ShowPreviousExpenses(user.WalletID, month * -1);
                    if (expenses != null)
                    {
                        return Ok(expenses);
                    }
                }
                return BadRequest(null);
            }
            return Unauthorized();
        }

        [HttpGet("getAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _context.ExpenseCategories.ToListAsync();
            return Ok(categories);
        }


        




        [HttpGet("getNameAndLimit")]
        public async Task<IActionResult> GetWalletTitleAndLimit(string userId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user != null)
                {
                    var walletData = await _expenseRepository.GetWalletData(user.WalletID);
                    if (walletData != null)
                        return Ok(walletData);
                }
                return BadRequest("Could not retreive wallet's data");
            }
            return Unauthorized();
        }


        //[HttpGet("getCategoryExpenses/{categoryId}")]
        //public async Task<IActionResult> GetCategoryExpenses(string userId, int categoryId)
        //{
        //    if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
        //    {
        //        if (categoryId != 0)
        //        {
        //            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        //            if (user != null)
        //            {
        //                var result = await _expenseRepository.ShowCategoryExpenses(user.WalletID, categoryId);
        //                if (result != null)
        //                {
        //                    return Ok(result);
        //                }
        //            }
        //            return BadRequest("User has not been found");
        //        }
        //        return BadRequest("There is no category");

        //    }

        //    return Unauthorized();
        //}

        [HttpPost("new")]
        public async Task<IActionResult> CreateExpense(string userId, Expense newExpense)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (user != null)
                {
                    ExpenseWithMessageDTO expenseWithMessage = new ExpenseWithMessageDTO();
                    var walletData = await _expenseRepository.GetWalletData(user.WalletID);
                    if (walletData.WalletCategories.Contains(newExpense.ExpenseCategoryId))
                    {
                        newExpense.ExpenseUserId = userId;
                        newExpense.FamilyWalletId = user.WalletID;
                        if (await _expenseRepository.CreateNewExpense(newExpense) != null)
                        {
                            var expenseSum = walletData.MonthlyExpenses + newExpense.MoneySpent;
                            var compareSum = 0.75 * walletData.MonthlyLimit;
                            var walletAdmin = await _userManager.Users.FirstOrDefaultAsync(u => u.IsWalletAdmin && u.WalletID == user.WalletID);
                            if (expenseSum > walletData.MonthlyLimit)
                            {
                                expenseWithMessage.Message = "You have exceeded your wallet's limit!";
                                await _notificationRepository.CreateNotification(user.Id, walletAdmin.Id, "Limit", expenseWithMessage.Message, false);
                            }
                            else if (expenseSum > (0.9 * walletData.MonthlyLimit))
                            {
                                expenseWithMessage.Message = "You have reached 90% of wallet's limit!";
                                await _notificationRepository.CreateNotification(user.Id, walletAdmin.Id, "90", expenseWithMessage.Message, false);
                            }
                            else if (expenseSum > compareSum)
                            {
                                expenseWithMessage.Message = "You have reached 75% of wallet's limit!!";
                                await _notificationRepository.CreateNotification(user.Id, walletAdmin.Id, "75", expenseWithMessage.Message, false);
                            }
                            ExpenseDTO expenseToReturn = new ExpenseDTO();
                            expenseToReturn = _mapper.Map<ExpenseDTO>(newExpense);
                            expenseToReturn.UserName = user.UserName;
                            expenseWithMessage.Expense = expenseToReturn;
                            return Ok(expenseWithMessage);
                        }
                        return BadRequest("Something went wrong");
                    }
                    return BadRequest("Non existing category");
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
        public async Task<IActionResult> EditExpense(string userId, int expenseId, ExpenseDTO expense)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (user != null)
                {
                    var expToEdit = await _expenseRepository.EditExpense(userId, expense);
                    if (expToEdit != null)
                        return Ok(expToEdit);
                    return BadRequest();
                }
                return BadRequest("No user has been found");
            }
            return Unauthorized();
        }

        [HttpGet("barExpenses/{month}")]
        public async Task<IActionResult> ShowBarExpensesData(string userId, int month)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user != null && user.WalletID != 0)
                {
                    var result = await _expenseRepository.CreateBarExpensesData(user.WalletID, month * -1);
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
                if (categoryId <= 0)
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