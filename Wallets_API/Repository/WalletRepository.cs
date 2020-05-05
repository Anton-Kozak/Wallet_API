using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.Data;
using Wallets_API.DBClasses;

namespace Wallets_API.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ApplicationDbContext _context;

        public WalletRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        

        public async Task<bool> CreateWallet(Wallet wallet)
        {
            _context.Wallets.Add(wallet);

            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<bool> InviteToWallet(string inviterId, string inviteeId, int walletId)
        {
            var inviter = await _context.Users.Where(u => u.Id == inviterId).FirstOrDefaultAsync();
            var invitee = await _context.Users.Where(u => u.Id == inviteeId).FirstOrDefaultAsync();
            if (inviter != invitee)
            {
                //TODO: сделать более информативную систему ошибок
                if (invitee.WalletID == 0)
                {
                    Request request = new Request();
                    request.InviteCreationTime = DateTime.Now;
                    request.RequestCreatorId = inviterId;
                    request.RequestReceiverEmail = invitee.Email;
                    request.WalletTitle = await _context.Wallets.Where(w => w.Id == walletId && w.WalletCreatorID == inviterId).Select(s => s.Title).FirstOrDefaultAsync();
                    request.WalletId = walletId;
                    request.TargetName = invitee.NormalizedUserName;
                    await _context.Requests.AddAsync(request);
                    if (await _context.SaveChangesAsync() > 0)
                        return true;
                }
                return false;
            }
            return false;
        }

        public async Task<bool> AcceptInvite(string userId)
        {
            var user = await _context.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            if(user.WalletID == 0)
            {
                var request = await _context.Requests.Where(r => r.RequestReceiverEmail == user.Email && r.TargetName == user.NormalizedUserName).FirstOrDefaultAsync();
                user.WalletID = request.WalletId;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                _context.Requests.Remove(request);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
