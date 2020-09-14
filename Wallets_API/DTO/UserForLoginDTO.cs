using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wallets_API.DTO
{
    public class UserForLoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Date { get; set; }
    }
}
