using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wallets_API.DBClasses
{
    public class Request
    {
        public int Id { get; set; }
        //кто приглашает/запрашивает
        public string RequestCreatorId { get; set; }
        //кого приглашают/запрашивают
        public string RequestReceiverEmail { get; set; }
        public DateTime InviteCreationTime { get; set; }
        public string WalletTitle { get; set; }
        public string TargetName { get; set; }
        public int WalletId { get; set; }
    }
}
