using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wallets_API.DTO
{
    public class InviteToReturnDTO
    {
        public int Id { get; set; }
        public string InviteCreatorEmail { get; set; }
        public string InviteReceiverEmail { get; set; }
        public DateTime InviteCreationTime { get; set; }
        public string WalletTitle { get; set; }
        public int WalletId { get; set; }
    }
}
