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
        private readonly IInviteRepository _repository;
        private readonly INotificationRepository _noteRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public InviteController(IInviteRepository repository, UserManager<ApplicationUser> userManager, INotificationRepository noteRepository)
        {
            _repository = repository;
            _userManager = userManager;
            _noteRepository = noteRepository;
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
                var whoInvites = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                var whoIsInvited = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == userToInviteEmail);
                var result = await _repository.InviteToWallet(userId, userToInviteEmail, whoInvites.WalletID);

                if (result.isSuccessful)
                {
                    await _noteRepository.CreateNotification(whoInvites.Id, whoIsInvited.Id, "NewMemberInvite", "Would you like to join my wallet?", false);
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
                
                var whoIsInvited = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                var whoInvites = await _userManager.Users.FirstOrDefaultAsync(u => u.WalletID == walletId && u.IsWalletAdmin);
                //TODO: исправить ВЕЗДЕ передачу userId, вместо того чтобы передавать сразу юзера
                var result = await _repository.AcceptInvite(userId, walletId);

                if (result.isSuccessful)
                {
                    await _noteRepository.DeleteRequestAndInviteNotifications(whoIsInvited);
                    await _noteRepository.CreateNotification(whoIsInvited.Id, whoInvites.Id, "NewMember", $"Member {whoIsInvited.UserName} has joined the wallet!", true); 
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