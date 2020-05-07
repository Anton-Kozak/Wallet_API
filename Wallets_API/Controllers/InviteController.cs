using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wallets_API.Models;
using Wallets_API.Repository;

namespace Wallets_API.Controllers
{
    [Route("api/[controller]/{userId}/")]
    [ApiController]
    public class InviteController : ControllerBase
    {
        private readonly IWalletRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public InviteController(IWalletRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [HttpGet("getInvites")]
        public async Task<IActionResult> GetInvitesToJoinWallet(string userId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user != null)
                {
                    var result = await _repository.GetInvites(userId);
                    return Ok(result);
                }
                return BadRequest("No invites have been found");
            }
            return Unauthorized();
        }


        [HttpPost("invite/{userToInviteEmail}")]
        public async Task<IActionResult> InviteNewUserToWallet(string userId, string userToInviteEmail)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                var result = await _repository.InviteToWallet(userId, userToInviteEmail, user.WalletID);

                if (result.isSuccessful)
                {
                    return Ok(result.Message);
                }
                return BadRequest(result.Message);
            }
            return Unauthorized();
        }

        [HttpPost("accept/{walletId}")]
        public async Task<IActionResult> AcceptInvitationToJoinWallet(string userId, int walletId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                var result = await _repository.AcceptInvite(userId, walletId);

                if (result.isSuccessful)
                {
                    return Ok(result.Message);
                }
                return BadRequest(result.Message);
            }
            return Unauthorized();
        }

        //TODO: подумать о том, чтобы передавать и ID кошелька и ID инвайта
        [HttpPost("decline/{walletId}")]
        public async Task<IActionResult> DeclineInvitationToJoinWallet(string userId, int walletId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                var result = await _repository.DeclineInvite(userId, walletId);

                if (result.isSuccessful)
                {
                    return Ok(result.Message);
                }
                return BadRequest(result.Message);
            }
            return Unauthorized();
        }
    }
}