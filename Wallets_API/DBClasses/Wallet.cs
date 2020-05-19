using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wallets_API.DBClasses
{
    public class Wallet
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public string WalletCreatorID { get; set; }
        public int MonthlyLimit { get; set; }
    }
}
