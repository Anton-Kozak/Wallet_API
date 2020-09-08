using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.DBClasses;
using Wallets_API.DTO;
using Wallets_API.Models;

namespace Wallets_API.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, UserToReturnAfterRegistrationDto>();
            CreateMap<WalletToCreateDTO, Wallet>();
            CreateMap<ApplicationUser, UserForDisplayDTO>();
            CreateMap<UserToReturnToAdmin, UserToReturnToAdminDTO>();
            CreateMap<Wallet, WalletToReturnDTO>();
            CreateMap<Expense, ExpenseDTO>();
            CreateMap<ApplicationUser, UserForProfileEditDTO>();
            CreateMap<UserForProfileEditDTO, ApplicationUser>();
        }
    }
}
