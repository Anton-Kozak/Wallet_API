using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wallets_API.DTO
{
    public class WalletToCreateDTO
    {
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public int MonthlyLimit { get; set; }
        public string Currency { get; set; }

        public WalletToCreateDTO()
        {
            CreationDate = DateTime.Now;
        }
    }
}
