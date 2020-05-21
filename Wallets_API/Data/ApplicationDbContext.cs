using Microsoft.AspNetCore.Identity;
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

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    builder.Entity<ApplicationUserRole>(userRole =>
        //    {
        //        userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

        //        userRole.HasOne(ur => ur.Role)
        //            .WithMany(r => r.UserRoles)
        //            .HasForeignKey(ur => ur.RoleId)
        //            .IsRequired();

        //        userRole.HasOne(ur => ur.User)
        //            .WithMany(r => r.UserRoles)
        //            .HasForeignKey(ur => ur.UserId)
        //            .IsRequired();
        //    });
        //}

    }
}
