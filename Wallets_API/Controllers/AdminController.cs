using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Wallets_API.DTO;
using Wallets_API.Models;
using Wallets_API.Repository;

namespace Wallets_API.Controllers
{
    [Authorize(Policy = "Admin")]
    [Route("api/[controller]/{userId}/")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _repo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AdminController(IAdminRepository repo, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _repo = repo;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        [HttpGet("getUsers")]
        public async Task<IActionResult> GetUsersData(string userId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user != null && user.IsWalletAdmin)
                {
                    var users = await _repo.GetUserData(user.WalletID);
                    var usersToReturn = _mapper.Map<UserToReturnToAdminDTO[]>(users);
                    return Ok(usersToReturn);
                }
                return BadRequest("You are not wallet's admin!");
            }
            return Unauthorized();
        }

        [HttpPost("removeUser/{removeUserId}")]
        public async Task<IActionResult> RemoveUser(string userId, string removeUserId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                var userToRemove = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == removeUserId);
                if (user != null && user.IsWalletAdmin && user.WalletID == userToRemove.WalletID && userToRemove != null)
                {
                    var result = await _repo.RemoveUserAsync(userToRemove, userToRemove.WalletID);
                    if (result)
                        return Ok($"User {userToRemove.UserName} has been removed");
                }
                return BadRequest("User has not been found");
            }
            return Unauthorized();
        }


        [HttpGet("getExpensesData")]
        public async Task<IActionResult> GetExpensesData(string userId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user != null && user.IsWalletAdmin)
                {
                    var expensesForAdmin = await _repo.GetAllExpenses(user.WalletID);
                    return Ok(expensesForAdmin);
                }
                return BadRequest("User has not been found");
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
                    var result = await _repo.DeleteExpense(expenseId);
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
                    var editedExpense = await _repo.EditExpense(expense);
                    if (editedExpense != null)
                        return Ok(editedExpense);
                    return BadRequest();
                }
                return BadRequest("No user has been found");
            }
            return Unauthorized();
        }
    }


}