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

    public class WalletController : ControllerBase
    {
        private readonly IWalletRepository _repo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public WalletController(IWalletRepository repo, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _repo = repo;
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }
        [Authorize(Policy = "Adult")]
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
                        //TODO: сделать разлогинивание чтобы вступили в действия смена роли
                        await _userManager.AddToRoleAsync(currentUser, "Admin");
                        return Ok(currentUser);
                    }
                    return BadRequest("Error with creating a wallet");
                }
                return BadRequest("No user has been found");
            }
            return Unauthorized();
        }

        [HttpPost("addCategories")]
        public async Task<IActionResult> AddCategoriesToWallet(string userId, [FromBody]int[] categories)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var currentUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (currentUser != null)
                {
                    if (currentUser.IsWalletAdmin && currentUser.WalletID != 0)
                    {
                        if (categories.Length > 0)
                        {
                            var res = await _repo.AddCategoriesToWallet(currentUser.WalletID, categories);
                            return Ok(res);
                        }
                        return BadRequest("You have not chosen any categories!");
                    }
                }
                return BadRequest("No user has been found");
            }
            return Unauthorized();
        }

        [HttpGet("getWalletCategories")]
        public async Task<IActionResult> GetWalletCategories(string userId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var currentUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (currentUser != null)
                {
                    var res = await _repo.GetCategories(currentUser.WalletID);
                    return Ok(res);
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
        [Authorize(Policy = "Adult")]
        [HttpPut("editWallet")]
        public async Task<IActionResult> EditWallet(string userId, WalletToReturnDTO walletToEdit)
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

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile(string userId)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var currentUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (currentUser != null && currentUser.WalletID != 0)
                {
                    ProfileDTO profile = new ProfileDTO();
                    profile.EditUser = _mapper.Map<UserForProfileEditDTO>(currentUser);
                    var profileDTO = await _repo.GetProfileInfo(profile, currentUser);
                    return Ok(profileDTO);
                }
                return BadRequest(null);
            }
            return Unauthorized();
        }

        [HttpPost("updateProfile")]
        public async Task<IActionResult> UpdateProfile(string userId, UserForProfileEditDTO editUser)
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier).Value == userId)
            {
                var currentUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                if (currentUser != null && currentUser.WalletID != 0)
                {
                    var results = await _repo.UpdateProfile(currentUser, editUser);
                    if (results.isSuccessful)
                    {
                        return Ok(results.Message);
                    }
                    return BadRequest(results.Message);
                }
                return BadRequest(null);
            }
            return Unauthorized();
        }

    }
}