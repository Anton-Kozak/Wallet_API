using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
        private readonly IWalletRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public RequestController(IWalletRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        //TODO: передавать имейл не через параметр, а как объект
        [HttpPost("request/{userToRequestEmail}")]
        public async Task<IActionResult> SendRequest(string userId, string userToRequestEmail)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                var result = await _repository.CreateRequestForAccess(userId, userToRequestEmail);
                if (result.isSuccessful)
                {
                    return Ok(result.Message);
                }
                return BadRequest(result.Message);
            }
            return Unauthorized();
        }


        [HttpGet("getRequests/{ownerEmail}")]
        public async Task<IActionResult> GetRequests(string userId, string ownerEmail)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId && u.Email == ownerEmail);
                if (user != null)
                {
                    var requests = await _repository.GetRequests(ownerEmail);
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
                if (user != null)
                {
                    var result = await _repository.AcceptRequest(emailToAccept, user.WalletID);
                    if (result.isSuccessful)
                        return Ok(result.Message);
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
                    return Ok(result.Message);
                }
                return BadRequest(result.Message);
            }
            return Unauthorized();
        }
    }
}