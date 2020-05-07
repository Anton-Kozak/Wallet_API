using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wallets_API.DBClasses
{
    public class Invite
    {
        public int Id { get; set; }
        public string InviteCreatorId { get; set; }
        public string InviteReceiverEmail { get; set; }
        public DateTime InviteCreationTime { get; set; }
        public string WalletTitle { get; set; }
        public int WalletId { get; set; }
    }
}
