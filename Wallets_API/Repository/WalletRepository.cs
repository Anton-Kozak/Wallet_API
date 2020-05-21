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
    public class WalletRepository : IWalletRepository
    {
        private readonly ApplicationDbContext _context;

        public WalletRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<bool> CreateWallet(Wallet wallet, ApplicationUser user)
        {
            _context.Wallets.Add(wallet);

            if (await _context.SaveChangesAsync() > 0)
            {
                user.WalletID = wallet.Id;
                user.DateJoined = DateTime.Now;
                user.IsWalletAdmin = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public async Task<Wallet> GetCurrentWallet(int walletId)
        {
            var walletToEdit = await _context.Wallets.Where(w => w.Id == walletId).FirstOrDefaultAsync();
            if (walletToEdit != null)
            {
                return walletToEdit;
            }
            return null;
        }

        public async Task<ResponseData> EditWallet(int walletId, WalletToReturnDTO walletToEdit)
        {
            ResponseData responseData = new ResponseData
            {
                isSuccessful = false,
                Message = "No wallet has been found",
            };
            var wallet = await _context.Wallets.Where(w => w.Id == walletId).FirstOrDefaultAsync();
            if (wallet != null)
            {
                wallet.Title = walletToEdit.Title;
                wallet.MonthlyLimit = walletToEdit.MonthlyLimit;
                _context.Wallets.Update(wallet);
                if (await _context.SaveChangesAsync() > 0)
                {
                    responseData.isSuccessful = true;
                    responseData.Message = "Edit was successful";
                    return responseData;
                }
                responseData.Message = "Edit was not successful";
                return responseData;
            }
            return responseData;

        }

        //--------------------------------------INVITE---------------------------------------------

        public async Task<IEnumerable<InviteToReturnDTO>> GetInvites(string userId)
        {
            var userEmail = await _context.Users.Where(u => u.Id == userId).Select(u => u.Email).FirstOrDefaultAsync();
            var invites = await (from i in _context.Invites
                                 join u in _context.Users on i.InviteCreatorId equals u.Id
                                 where i.InviteReceiverEmail == userEmail
                                 select new InviteToReturnDTO
                                 {
                                     Id = i.Id,
                                     InviteCreationTime = i.InviteCreationTime,
                                     WalletId = i.WalletId,
                                     InviteCreatorEmail = u.Email,
                                     InviteReceiverEmail = userEmail,
                                     WalletTitle = i.WalletTitle
                                 }
                                ).ToListAsync();
            return invites;
        }

        public async Task<ResponseData> InviteToWallet(string inviterId, string inviteeEmail, int walletId)
        {
            ResponseData responseData = new ResponseData
            {
                isSuccessful = false,
                Message = "User is incorrect or you do not have a wallet",
            };

            var inviter = await _context.Users.Where(u => u.Id == inviterId).FirstOrDefaultAsync();
            var invitee = await _context.Users.Where(u => u.Email == inviteeEmail).FirstOrDefaultAsync();
            if (invitee != null && inviter != null && inviter.WalletID != 0)
            {
                if (inviter != invitee)
                {
                    var hasInviteToThisWallet = await _context.Invites.Where(i => i.WalletId == walletId && i.InviteReceiverEmail == inviteeEmail).AnyAsync();
                    if (!hasInviteToThisWallet)
                    {
                        //TODO: сделать более информативную систему ошибок
                        if (invitee.WalletID == 0)
                        {
                            Invite invite = new Invite();
                            invite.InviteCreationTime = DateTime.Now;
                            invite.InviteCreatorId = inviterId;
                            invite.InviteReceiverEmail = invitee.Email;
                            invite.WalletTitle = await _context.Wallets.Where(w => w.Id == walletId && w.WalletCreatorID == inviterId).Select(s => s.Title).FirstOrDefaultAsync();
                            invite.WalletId = walletId;
                            await _context.Invites.AddAsync(invite);
                            if (await _context.SaveChangesAsync() > 0)
                            {
                                responseData.isSuccessful = true;
                                responseData.Message = $"You have sent an invitation to {inviteeEmail}";
                                return responseData;
                            }
                        }
                        responseData.Message = "User already has a wallet";
                        return responseData;
                    }
                    responseData.Message = "User already has an invite to this wallet";
                    return responseData;
                }
            }
            return responseData;
        }

        public async Task<ResponseData> AcceptInvite(string userId, int walletId)
        {
            ResponseData responseData = new ResponseData
            {
                isSuccessful = false,
                Message = "User alread has a wallet",
            };

            var user = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            if (user.WalletID == 0)
            {
                var invite = await _context.Invites.Where(i => i.InviteReceiverEmail == user.Email && i.WalletId == walletId).FirstOrDefaultAsync();
                if (invite != null)
                {
                    user.WalletID = walletId;
                    user.DateJoined = DateTime.Now;
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                    responseData.isSuccessful = true;
                    responseData.Message = $"You have joined wallet - {invite.WalletTitle}";
                    var invitesToRemove = await _context.Invites.Where(i => i.InviteReceiverEmail == user.Email).ToListAsync();
                    _context.Invites.RemoveRange(invitesToRemove);
                    await _context.SaveChangesAsync();
                    return responseData;
                }
                responseData.Message = "There is no such an invite";
                return responseData;
            }
            return responseData;
        }

        public async Task<ResponseData> DeclineInvite(string userId, int walletId)
        {
            ResponseData responseData = new ResponseData
            {
                isSuccessful = false,
                Message = "User alread has a wallet",
            };
            var user = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();

            if (user.WalletID == 0)
            {
                var inviteToDecline = await _context.Invites.Where(i => i.WalletId == walletId && i.InviteReceiverEmail == user.Email).ToListAsync();
                if (inviteToDecline.Count() == 1)
                {
                    _context.Invites.Remove(inviteToDecline.First());
                    await _context.SaveChangesAsync();
                    responseData.isSuccessful = true;
                    responseData.Message = $"You have declined invite to {inviteToDecline.First().WalletTitle}";
                    return responseData;
                }
                else
                {
                    _context.Invites.RemoveRange(inviteToDecline);
                    await _context.SaveChangesAsync();
                    responseData.Message = "There was several invites to this email. All of them were deleted";
                    return responseData;
                }
            }
            return responseData;
        }



        //--------------------------------------REQUEST---------------------------------------------

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
                if (walletOwner.WalletID != 0)
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
                    }
                    return responseData;
                }
                responseData.Message = $"The user {userToRequestEmail} does not have a wallet";
                return responseData;
            }
            return responseData;
        }

        public async Task<IEnumerable<RequestToReturnDTO>> GetRequests(string ownerEmail)
        {
            var requests = await (from r in _context.Requests
                                  join u in _context.Users on r.RequestCreatorId equals u.Id
                                  where r.RequestReceiverEmail == ownerEmail
                                  select new RequestToReturnDTO
                                  {
                                      Id = r.Id,
                                      RequestCreatorEmail = u.Email,
                                      InviteCreationTime = r.InviteCreationTime.ToString()
                                  }).ToListAsync();
            return requests;
        }

        public async Task<ResponseData> AcceptRequest(string emailToAccept, int walletId)
        {
            ResponseData responseData = new ResponseData
            {
                isSuccessful = false,
                Message = "User has not been found",
            };

            var user = await _context.Users.Where(u => u.Email == emailToAccept).FirstOrDefaultAsync();
            if (user != null)
            {
                var request = await _context.Requests.Where(r => r.RequestCreatorId == user.Id).FirstOrDefaultAsync();
                if (request != null)
                {
                    user.WalletID = walletId;
                    user.DateJoined = DateTime.Now;
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    _context.Requests.Remove(request);
                    await _context.SaveChangesAsync();
                    responseData.isSuccessful = true;
                    responseData.Message = $"You have successfully accepted a request from {emailToAccept}";
                    return responseData;
                }
                responseData.Message = "Request has not been found";
                return responseData;
            }
            return responseData;
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
