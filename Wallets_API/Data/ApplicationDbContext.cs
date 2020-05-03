﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.DBClasses;
using Wallets_API.Models;

namespace Wallets_API.Data{
        public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

        //public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
    }
}
