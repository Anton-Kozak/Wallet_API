using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wallets_API.DBClasses;
using Wallets_API.Models;

namespace Wallets_API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Invite> Invites { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<NotificationUser> NotificationsUsers { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationCategory> NotificationCategories { get; set; }
        public DbSet<WalletsCategories> WalletsCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<NotificationUser>()
                .HasKey(nu => new { nu.NotificationId, nu.UserId });

            builder.Entity<WalletsCategories>()
                .HasKey(wc => new { wc.WalletId, wc.CategoryId });
        }

    }
}
