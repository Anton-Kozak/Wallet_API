using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wallets_API.DTO
{
    public class RequestToReturnDTO
    {
        public int Id { get; set; }
        public string RequestCreatorEmail { get; set; }
        public string InviteCreationTime { get; set; }
    }
}
