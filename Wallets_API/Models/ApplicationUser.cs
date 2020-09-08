using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Wallets_API.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Age { get; set; }
        public int WalletID { get; set; }
        public bool IsWalletAdmin { get; set; }
        public DateTime DateJoined { get; set; }
        public int UserPhotoId { get; set; }
    }
}
