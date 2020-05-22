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
        Task<Notification> Notification(string userId);
        Task<List<Notification>> GetNotifications(ApplicationUser user);
    }
}
