using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Wallets_API.DBClasses;
using Wallets_API.DTO;
using Wallets_API.Models;
using Wallets_API.Repository;

namespace Wallets_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        [HttpPost("{userId}/createwallet")]
        public async Task<IActionResult> CreateWallet(WalletToCreateDTO walletToCreate, string userId)
        {
            var currentUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (currentUser != null)
            {
                var walletToSave = _mapper.Map<Wallet>(walletToCreate);
                walletToSave.WalletCreatorID = userId;
                if (await _repo.CreateWallet(walletToSave))
                {
                    currentUser.WalletID = walletToSave.Id;
                    return Ok("The wallet has been created!");

                }
                return BadRequest("Error with creating a wallet");
            }
            return Unauthorized();
        }
    }
}