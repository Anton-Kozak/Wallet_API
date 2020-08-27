using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.Data;
using Wallets_API.DBClasses;
using Wallets_API.DTO;
using Wallets_API.Models;
using Wallets_API.Models.CustomModels;

namespace Wallets_API.Repository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly ApplicationDbContext _context;


        public RequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseData> CreateRequestForAccess(string requesterId, string userToRequestEmail)
        {
            ResponseData responseData = new ResponseData
            {
                isSuccessful = false,
                Message = "User is incorrect",
            };

            var requester = await _context.Users.Where(u => u.Id == requesterId).FirstOrDefaultAsync();
            var walletOwner = await _context.Users.Where(u => u.Email == userToRequestEmail).FirstOrDefaultAsync();

            if (walletOwner != null)
            {
                if (walletOwner.WalletID != 0 && walletOwner.IsWalletAdmin)
                {
                    var wallet = await _context.Wallets.Where(w => w.Id == walletOwner.WalletID).FirstOrDefaultAsync();
                    if (wallet.UserNumber < 5)
                    {
                        bool hasRequests = await _context.Requests.Where(r => r.RequestCreatorId == requesterId && r.WalletId == walletOwner.WalletID).AnyAsync();
                        if (hasRequests)
                        {
                            responseData.Message = "You have already requested access to this wallet!";
                            return responseData;
                        }
                        if (requester != walletOwner)
                        {
                            Request request = new Request();
                            request.InviteCreationTime = DateTime.Now;
                            request.RequestCreatorId = requesterId;
                            request.RequestReceiverEmail = userToRequestEmail;
                            request.WalletId = walletOwner.WalletID;
                            await _context.Requests.AddAsync(request);
                            if (await _context.SaveChangesAsync() > 0)
                            {
                                responseData.isSuccessful = true;
                                responseData.Message = $"You have successfully sent a request to {userToRequestEmail}";
                                return responseData;
                            }
                            responseData.isSuccessful = false;
                            responseData.Message = $"Notification was not created";
                            return responseData;
                        }
                    }
                    responseData.Message = "Wallet has already max users";
                    return responseData;
                }
                responseData.Message = "User has no wallet or is not a wallet admin";
                return responseData;
            }
            responseData.Message = $"The user {userToRequestEmail} does not have a wallet";
            return responseData;
        }

        public async Task<IEnumerable<RequestToReturnDTO>> GetRequests(string ownerEmail)
        {
            var reqsToRemove = await (from r in _context.Requests
                                      join u in _context.Users on r.RequestCreatorId equals u.Id
                                      where r.RequestReceiverEmail == ownerEmail
                                      where u.WalletID != 0
                                      select new Request
                                      {
                                          Id = r.Id,
                                          RequestCreatorId = r.RequestCreatorId,
                                          RequestReceiverEmail = r.RequestReceiverEmail,
                                          InviteCreationTime = r.InviteCreationTime,
                                          WalletId = r.WalletId
                                      }).ToListAsync();
            _context.Requests.RemoveRange(reqsToRemove);
            await _context.SaveChangesAsync();
            var requests = await (from r in _context.Requests
                                  join u in _context.Users on r.RequestCreatorId equals u.Id
                                  where r.RequestReceiverEmail == ownerEmail
                                  select new RequestToReturnDTO
                                  {
                                      Id = r.Id,
                                      RequestCreatorEmail = u.Email,
                                      InviteCreationTime = r.InviteCreationTime.ToString()
                                  }).ToListAsync();
            List<int> requestsToRemove = new List<int>();
            for (int i = 0; i < requests.Count; i++)
            {
                var usersWithWallet = await _context.Users.Where(u => u.Email == requests[i].RequestCreatorEmail && u.WalletID != 0).FirstOrDefaultAsync();
                if (usersWithWallet != null)
                    requestsToRemove.Add(i);
            }
            foreach (int index in requestsToRemove.Reverse<int>())
            {
                requests.RemoveAt(index);
            }


            return requests;
        }

        public async Task<ResponseData> AcceptRequest(string emailToAccept, int walletId)
        {
            ResponseData responseData = new ResponseData
            {
                isSuccessful = false,
                Message = "User has not been found",
            };
            var wallet = await _context.Wallets.Where(w => w.Id == walletId).FirstOrDefaultAsync();
            if (wallet.UserNumber < 5)
            {
                var user = await _context.Users.Where(u => u.Email == emailToAccept).FirstOrDefaultAsync();
                if (user != null)
                {
                    var request = await _context.Requests.Where(r => r.RequestCreatorId == user.Id && r.WalletId == walletId).FirstOrDefaultAsync();
                    if (request != null)
                    {
                        user.WalletID = walletId;
                        user.DateJoined = DateTime.Now;
                        _context.Update(user);
                        responseData.isSuccessful = true;
                        await RemoveInvitesAndRequests(user);
                        wallet.UserNumber++;
                        await _context.SaveChangesAsync();
                        responseData.Message = $"You have successfully accepted a request from {emailToAccept}";

                        return responseData;
                    }
                    responseData.Message = "Request has not been found";
                    return responseData;
                }
                return responseData;
            }
            responseData.Message = "Wallet has reached its user limit";
            return responseData;
        }


        private async Task RemoveInvitesAndRequests(ApplicationUser user)
        {
            var invites = await _context.Invites.Where(i => i.InviteReceiverEmail == user.Email).ToListAsync();
            _context.Invites.RemoveRange(invites);

            var requests = await _context.Requests.Where(i => i.RequestCreatorId == user.Id).ToListAsync();
            _context.Requests.RemoveRange(requests);
            await _context.SaveChangesAsync();
        }


        public async Task<ResponseData> DeclineRequest(string userId, string emailToDecline)
        {
            ResponseData responseData = new ResponseData
            {
                isSuccessful = false,
                Message = "User has not been found",
            };

            var walletOwner = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            var userToDecline = await _context.Users.Where(u => u.Email == emailToDecline).FirstOrDefaultAsync();

            if (walletOwner != null && userToDecline != null)
            {
                var request = await _context.Requests.Where(r => r.RequestCreatorId == userToDecline.Id && r.RequestReceiverEmail == walletOwner.Email).FirstOrDefaultAsync();
                if (request != null)
                {
                    var notificationToDelete = await _context.Notifications.Where(n => n.InitiatorUser == userToDecline.Id && n.TargetUser == walletOwner.Id).FirstOrDefaultAsync();
                    if (notificationToDelete != null)
                    {
                        _context.Notifications.Remove(notificationToDelete);
                        var notificationUserToDelete = await _context.NotificationsUsers.Where(n => n.NotificationId == notificationToDelete.Id).FirstOrDefaultAsync();
                        if (notificationUserToDelete != null)
                            _context.NotificationsUsers.Remove(notificationUserToDelete);
                    }
                    _context.Requests.Remove(request);
                    await _context.SaveChangesAsync();
                    responseData.isSuccessful = true;
                    responseData.Message = "Request has been successfully declined";
                    return responseData;
                }
                responseData.Message = "Request has not been found";
                return responseData;
            }

            return responseData;

        }
    }
}
