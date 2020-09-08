using System;
using System.Collections.Generic;

namespace Wallets_API.DTO
{
    public class ProfileDTO
    {
        public UserForProfileEditDTO EditUser { get; set; }
        public DateTime DateJoined { get; set; }
        public double MoneySpent { get; set; }
        public List<WalletUsersForProfileDTO> WalletUsers { get; set; }
    }
}
