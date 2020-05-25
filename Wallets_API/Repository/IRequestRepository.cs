using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.DTO;
using Wallets_API.Models.CustomModels;

namespace Wallets_API.Repository
{
    public interface IRequestRepository
    {
        Task<ResponseData> CreateRequestForAccess(string requesterId, string userToRequestEmail);
        Task<IEnumerable<RequestToReturnDTO>> GetRequests(string ownerEmail);
        Task<ResponseData> AcceptRequest(string emailToAccept, int walletId);
        Task<ResponseData> DeclineRequest(string userId, string emailToDecline);
    }
}
