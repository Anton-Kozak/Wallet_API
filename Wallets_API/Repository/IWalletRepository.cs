using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.DBClasses;
using Wallets_API.DTO;
using Wallets_API.Models;
using Wallets_API.Models.CustomModels;

namespace Wallets_API.Repository
{
    public interface IWalletRepository
    {
        Task<bool> CreateWallet(Wallet wallet, ApplicationUser user);


        Task<ResponseData> InviteToWallet(string inviterId, string inviteeEmail, int walletId);
        Task<ResponseData> AcceptInvite(string userId, int walletId);
        Task<ResponseData> DeclineInvite(string userId, int walletId);
        Task<IEnumerable<InviteToReturnDTO>> GetInvites(string userId);

        Task<ResponseData> CreateRequestForAccess(string requesterId, string userToRequestEmail);
        Task<IEnumerable<RequestToReturnDTO>> GetRequests(string ownerEmail);
        Task<ResponseData> AcceptRequest(string emailToAccept, int walletId);
        Task<ResponseData> DeclineRequest(string userId, string emailToDecline);
    }
}
