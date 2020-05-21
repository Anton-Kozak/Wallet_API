using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.Data;
using Wallets_API.DTO;
using Wallets_API.Models;

namespace Wallets_API.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public AdminRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<List<UserToReturnToAdmin>> GetUserData(int walletId)
        {
            //TODO: добавить роли
            var users = await (from u in _context.Users
                               where u.WalletID == walletId
                               select new UserToReturnToAdmin
                               {
                                   Id = u.Id,
                                   Address = u.Address,
                                   Age = u.Age,
                                   DateJoined = u.DateJoined,
                                   Username = u.UserName,
                                   User = u,
                               }).ToListAsync();
            foreach (var user in users)
            {
                user.UserRoles = await _userManager.GetRolesAsync(user.User);
            }
            return users;
        }

        public async Task<bool> RemoveUserAsync(ApplicationUser userToRemove)
        {
            userToRemove.WalletID = 0;
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
