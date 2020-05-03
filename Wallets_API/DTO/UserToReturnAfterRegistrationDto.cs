using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wallets_API.DTO
{
    public class UserToReturnAfterRegistrationDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Age { get; set; }
        public string Address { get; set; }
    }
}
