using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wallets_API.Models;
using Wallets_API.Repository;

namespace Wallets_API.Controllers
{
    //TODO: сделать проверку на то, является ли пользователь тем кем нужно
    [Route("api/[controller]/{userId}/")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IWalletRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public RequestController(IWalletRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        
        [HttpPost("invite/{userToInviteId}")]
        public async Task<IActionResult> InviteToWallet(string userId, string userToInviteId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var result = await _repository.InviteToWallet(userId, userToInviteId, user.WalletID);

            if (result)
            {
                return Ok($"You have successfully invited {userToInviteId}");
            }
            return BadRequest("Something went wrong");
        }

        [HttpPost("accept")]
        public async Task<IActionResult> AcceptInvitation(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var result = await _repository.AcceptInvite(userId);

            if (result)
            {
                return Ok($"You have successfully accepted an invitation");
            }
            return BadRequest("Something went wrong");
        }
    }
}