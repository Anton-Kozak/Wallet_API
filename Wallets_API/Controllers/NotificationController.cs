using AutoMapper;
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
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _repo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public NotificationController(INotificationRepository repo, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _repo = repo;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        [HttpGet("getNotifications")]
        public async Task<IActionResult> GetNotifications(string userId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var currentUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (currentUser != null)
                {
                    var notes = await _repo.GetNotifications(currentUser);
                    return Ok(notes);
                }
                return BadRequest("error");
            }
            return Unauthorized();
        }

        [HttpPost("deleteNotification")]
        public async Task<IActionResult> DeleteNotification(string userId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var currentUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (currentUser != null && currentUser.WalletID != 0)
                {
                    await _repo.DeleteNotification(currentUser);
                    return Ok();
                }
                return BadRequest("error");
            }
            return Unauthorized();
        }
    }

}