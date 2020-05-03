using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.DTO;
using Wallets_API.Models;

namespace Wallets_API.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, UserToReturnAfterRegistrationDto>();
        }
    }
}
