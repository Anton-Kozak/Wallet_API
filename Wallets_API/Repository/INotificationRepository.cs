using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.DBClasses;
using Wallets_API.Models;

namespace Wallets_API.Repository
{
    public interface INotificationRepository
    {
        Task<Notification> CreateNotification(string userId, string targetId, string notificationReason, string message, bool isForAll);
        Task<List<Notification>> GetNotifications(ApplicationUser user);
        Task DeleteNotification(ApplicationUser user);
        Task DeleteSpecificNotification(ApplicationUser user, string notificationReason, string targetId);
        Task DeleteRequestAndInviteNotifications(ApplicationUser user);
    }
}
