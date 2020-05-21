using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.DTO;
using Wallets_API.Models;
using Wallets_API.Models.CustomModels;

namespace Wallets_API.Repository
{
    public interface IAdminRepository
    {
        Task<List<UserToReturnToAdmin>> GetUserData(int walletId);
        Task<bool> RemoveUserAsync(ApplicationUser userToRemove);
    }
}
