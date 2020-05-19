using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Wallets_API.DBClasses;
using Wallets_API.DTO;
using Wallets_API.Models;
using Wallets_API.Repository;

namespace Wallets_API.Controllers
{
    [Route("api/[controller]/{userId}/")]
    [ApiController]
    [Authorize(Policy = "Adult")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletRepository _repo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public WalletController(IWalletRepository repo, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _repo = repo;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost("createwallet")]
        public async Task<IActionResult> CreateWallet([FromBody]WalletToCreateDTO walletToCreate, string userId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var currentUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (currentUser != null)
                {
                    var walletToSave = _mapper.Map<Wallet>(walletToCreate);
                    walletToSave.WalletCreatorID = userId;
                    if (await _repo.CreateWallet(walletToSave, currentUser))
                    {
                        return Ok(currentUser);
                    }
                    return BadRequest("Error with creating a wallet");
                }
                return BadRequest("No user has been found");
            }
            return Unauthorized();
        }

        [HttpGet("getCurrentWallet")]
        public async Task<IActionResult> GetCurrentWallet(string userId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var currentUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (currentUser != null)
                {
                    var wallet = await _repo.GetCurrentWallet(currentUser.WalletID);
                    if (wallet != null && currentUser.WalletID != 0)
                    {
                        var walletToReturn = _mapper.Map<WalletToReturnDTO>(wallet);
                        return Ok(walletToReturn);
                    }
                    return BadRequest("Wallet was not found");
                }
            }
            return Unauthorized();
        }

        [HttpPut("editWallet")]
        public async Task<IActionResult> GetCurrentWallet(string userId, WalletToReturnDTO walletToEdit)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var currentUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (currentUser != null && currentUser.WalletID != 0)
                {
                    var results = await _repo.EditWallet(currentUser.WalletID, walletToEdit);
                    if (results.isSuccessful)
                    {
                        return Ok(results.Message);
                    }
                    return BadRequest(results.Message);
                }
            }
            return Unauthorized();
        }
    }
}