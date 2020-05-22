using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.Data;
using Wallets_API.DBClasses;
using Wallets_API.Models;

namespace Wallets_API.Repository
{
    public class NotificationRepository: INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Notification> Notification(string userId)
        {
            var wallet = await _context.Users.Where(u => u.Id == userId).Select(u => u.WalletID).FirstOrDefaultAsync();
            var users = await _context.Users.Where(u => u.WalletID == wallet).ToListAsync();
            Notification note = new Notification
            {
                InitiatorUser = userId,
                CreationDate = DateTime.Now,
                Message = "New member",
                ReasonId = 2,
                WalletId = wallet,
                isForAll = true,
            };
            _context.Notifications.Add(note);
            await _context.SaveChangesAsync();

            foreach (var user in users)
            {
                NotificationUser notificationUser = new NotificationUser
                {
                    NotificationId = note.Id,
                    UserId = user.Id,
                };
                _context.NotificationsUsers.Add(notificationUser);


            }
            await _context.SaveChangesAsync();
            return note;
        }

        public async Task<List<Notification>> GetNotifications(ApplicationUser user)
        {
            var notificationsForUser = await (from n in _context.Notifications
                                              join nu in _context.NotificationsUsers
                                              on n.Id equals nu.NotificationId
                                              where nu.UserId == user.Id
                                              select n).ToListAsync();
            return notificationsForUser;
        }

    }
}
