using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.DTO;
using Wallets_API.Models.CustomModels;

namespace Wallets_API.Repository
{
    public interface IInviteRepository
    {
        Task<ResponseData> InviteToWallet(string inviterId, string inviteeEmail, int walletId);
        Task<ResponseData> AcceptInvite(string userId, int walletId);
        Task<ResponseData> DeclineInvite(string userId, int walletId);
        Task<IEnumerable<InviteToReturnDTO>> GetInvites(string userId);
    }
}
