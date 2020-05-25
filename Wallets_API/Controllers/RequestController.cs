using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Wallets_API.Models;
using Wallets_API.Repository;

namespace Wallets_API.Controllers
{
    [Route("api/[controller]/{userId}/")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestRepository _repository;
        private readonly INotificationRepository _noteRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public RequestController(IRequestRepository repository, UserManager<ApplicationUser> userManager, INotificationRepository noteRepository)
        {
            _repository = repository;
            _userManager = userManager;
            _noteRepository = noteRepository;
        }

        //TODO: передавать имейл не через параметр, а как объект
        [HttpPost("request/{userToRequestEmail}")]
        public async Task<IActionResult> SendRequest(string userId, string userToRequestEmail)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var requester = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                var walletOwner = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == userToRequestEmail);
                var result = await _repository.CreateRequestForAccess(userId, userToRequestEmail);
                if (result.isSuccessful)
                {
                    await _noteRepository.CreateNotification(requester.Id, walletOwner.Id, "NewMemberRequest", $"{requester.UserName} would like to join your wallet", false);
                    return Ok(result.Message);
                }
                return BadRequest(result.Message);
            }
            return Unauthorized();
        }


        [HttpGet("getRequests")]
        public async Task<IActionResult> GetRequests(string userId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (user != null)
                {
                    var requests = await _repository.GetRequests(user.Email);
                    return Ok(requests);
                }
                return BadRequest("User is not correct");
            }
            return Unauthorized();
        }

        [HttpPost("acceptRequest/{emailToAccept}")]
        public async Task<IActionResult> AcceptRequestAndGiveAccess(string userId, string emailToAccept)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                var userToAccept = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == emailToAccept);
                if (user != null)
                {
                    var result = await _repository.AcceptRequest(emailToAccept, user.WalletID);
                    if (result.isSuccessful)
                    {
                        await _noteRepository.DeleteRequestAndInviteNotifications(userToAccept);
                        await _noteRepository.CreateNotification(user.Id, userToAccept.Id, "NewMember", $"Member {userToAccept.UserName} has joined the wallet!", true);
                        return Ok(result.Message);
                    }
                    return BadRequest(result.Message);
                }
                return BadRequest("User is not correct");
            }
            return Unauthorized();
        }

        [HttpPost("decline/{emailToDecline}")]
        public async Task<IActionResult> DeclineRequestToAccess(string userId, string emailToDecline)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var result = await _repository.DeclineRequest(userId, emailToDecline);
                if (result.isSuccessful)
                {
                    var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                    var userToDecline = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == emailToDecline);
                    await _noteRepository.DeleteSpecificNotification(userToDecline, "NewMemberRequest", user.Id);
                    await _noteRepository.CreateNotification(user.Id, userToDecline.Id, "RequestDecline", $"You have been decline to join {user.UserName}'s wallet", false);
                    return Ok(result.Message);
                }
                return BadRequest(result.Message);
            }
            return Unauthorized();
        }
    }
}