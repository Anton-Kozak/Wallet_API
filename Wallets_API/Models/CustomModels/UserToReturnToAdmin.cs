using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.Models;

namespace Wallets_API.DTO
{
    public class UserToReturnToAdmin
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public DateTime DateJoined { get; set; }
        public string Address { get; set; }
        public string Age { get; set; }
        public IList<string> UserRoles { get; set; }
        public ApplicationUser User { get; set; }
    }
}
