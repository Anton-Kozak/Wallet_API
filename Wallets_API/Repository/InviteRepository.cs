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
    class InviteRepository : IInviteRepository
    {
        private readonly ApplicationDbContext _context;

        public InviteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

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
            var wallet = await _context.Wallets.Where(w => w.Id == walletId).FirstOrDefaultAsync();
            if (wallet.UserNumber < 5)
            {
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
            responseData.Message = "The wallet cannot have more than 5 users";
            return responseData;
        }

        public async Task<ResponseData> AcceptInvite(string userId, int walletId)
        {
            ResponseData responseData = new ResponseData
            {
                isSuccessful = false,
                Message = "User alread has a wallet",
            };
            var wallet = await _context.Wallets.Where(w => w.Id == walletId).FirstOrDefaultAsync();
            if (wallet.UserNumber < 5)
            {
                var whoIsInvited = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
                if (whoIsInvited.WalletID == 0)
                {
                    var whoInvites = await _context.Invites.Where(i => i.InviteReceiverEmail == whoIsInvited.Email && i.WalletId == walletId).FirstOrDefaultAsync();
                    if (whoInvites != null)
                    {
                        whoIsInvited.WalletID = walletId;
                        whoIsInvited.DateJoined = DateTime.Now;
                        _context.Users.Update(whoIsInvited);
                        responseData.isSuccessful = true;
                        responseData.Message = $"You have joined wallet - {whoInvites.WalletTitle}";
                        await RemoveInvitesAndRequests(whoIsInvited);
                        wallet.UserNumber++;
                        await _context.SaveChangesAsync();
                        return responseData;
                    }
                    responseData.Message = "There is no such an invite";
                    return responseData;
                }
                return responseData;
            }
            responseData.Message = "Wallet has reached the user limit";
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
                var inviteToDecline = await _context.Invites.Where(i => i.WalletId == walletId && i.InviteReceiverEmail == user.Email).FirstOrDefaultAsync();
                var notificationToDelete = await _context.Notifications.Where(n => n.ReasonId == 6 && n.InitiatorUser == inviteToDecline.InviteCreatorId && n.TargetUser == user.Id).FirstOrDefaultAsync();
                _context.Notifications.Remove(notificationToDelete);
                var notificationUserToDelete = await _context.NotificationsUsers.Where(n => n.NotificationId == notificationToDelete.Id).FirstOrDefaultAsync();
                _context.NotificationsUsers.Remove(notificationUserToDelete);
                _context.Invites.Remove(inviteToDecline);
                await _context.SaveChangesAsync();
                responseData.isSuccessful = true;
                responseData.Message = $"You have declined invite to {inviteToDecline.WalletTitle}";
                return responseData;
            }
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

    }
}
