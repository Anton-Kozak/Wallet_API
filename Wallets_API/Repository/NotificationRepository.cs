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
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Notification> CreateNotification(string userId, string targetId, string notificationReason, string message, bool isForAll)
        {
            var walletId = await _context.Users.Where(u => u.Id == targetId).Select(u => u.WalletID).FirstOrDefaultAsync();
            var notificationReasonId = await _context.NotificationCategories.Where(n => n.Title == notificationReason).Select(n => n.Id).FirstOrDefaultAsync();
            var exists = await _context.Notifications.Where(n => n.ReasonId == notificationReasonId && n.WalletId == walletId && n.InitiatorUser == userId && n.TargetUser == targetId).AnyAsync();
            if (!exists)
            {
                Notification note = new Notification
                {
                    InitiatorUser = userId,
                    TargetUser = targetId,
                    CreationDate = DateTime.Now,
                    Message = message,
                    ReasonId = notificationReasonId,
                    WalletId = walletId,
                    isForAll = isForAll,
                };
                _context.Notifications.Add(note);
                await _context.SaveChangesAsync();
                if (walletId != 0 && isForAll)
                {
                    List<ApplicationUser> users = await _context.Users.Where(u => u.WalletID == walletId).ToListAsync();
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
                }
                else
                {
                    NotificationUser notificationUser = new NotificationUser
                    {
                        NotificationId = note.Id,
                        UserId = targetId,
                    };
                    _context.NotificationsUsers.Add(notificationUser);
                    await _context.SaveChangesAsync();
                }
                return note;
            }
            return null;
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

        public async Task DeleteNotification(ApplicationUser user)
        {
            var notificationsToDelete = await _context.NotificationsUsers.Where(n => n.UserId == user.Id).ToListAsync();
            if (notificationsToDelete.Count > 0)
            {
                _context.NotificationsUsers.RemoveRange(notificationsToDelete);

                await _context.SaveChangesAsync();
                foreach (var notification in notificationsToDelete)
                {
                    var notificationsWithTheSameID = await _context.NotificationsUsers.Where(n => n.NotificationId == notification.NotificationId).AnyAsync();
                    if (!notificationsWithTheSameID)
                    {
                        var mainNotification = await _context.Notifications.Where(n => n.Id == notification.NotificationId).FirstOrDefaultAsync();
                        _context.Notifications.Remove(mainNotification);
                        await _context.SaveChangesAsync();
                    }
                }

            }
        }

        public async Task DeleteSpecificNotification(ApplicationUser user, string notificationReason, string targetId)
        {
            var reasonId = await _context.NotificationCategories.Where(c => c.Title == notificationReason).Select(n => n.Id).FirstOrDefaultAsync();
            var noteId = await _context.Notifications.Where(n => n.InitiatorUser == user.Id && n.ReasonId == reasonId && n.TargetUser == targetId).Select(n => n.Id).FirstOrDefaultAsync();

            var notificationsToDelete = await _context.NotificationsUsers.Where(n => n.UserId == targetId && n.NotificationId == noteId).ToListAsync();
            if (notificationsToDelete.Count > 0)
            {
                _context.NotificationsUsers.RemoveRange(notificationsToDelete);

                await _context.SaveChangesAsync();
                foreach (var notification in notificationsToDelete)
                {
                    var notificationsWithTheSameID = await _context.NotificationsUsers.Where(n => n.NotificationId == notification.NotificationId).AnyAsync();
                    if (!notificationsWithTheSameID)
                    {
                        var mainNotification = await _context.Notifications.Where(n => n.Id == notification.NotificationId).FirstOrDefaultAsync();
                        _context.Notifications.Remove(mainNotification);
                        await _context.SaveChangesAsync();
                    }
                }

            }
        }
    }
}
