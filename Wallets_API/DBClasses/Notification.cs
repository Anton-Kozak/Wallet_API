using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Wallets_API.DBClasses
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public int ReasonId { get; set; }
        public string Message { get; set; }
        public int WalletId { get; set; }
        public string InitiatorUser { get; set; }
        public bool isForAll { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
