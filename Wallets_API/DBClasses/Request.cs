﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wallets_API.DBClasses
{
    public class Request
    {
        public int Id { get; set; }
        public string RequestCreatorId { get; set; }
        public string RequestReceiverEmail { get; set; }
        public DateTime InviteCreationTime { get; set; }
        public int WalletId { get; set; }
    }
}
