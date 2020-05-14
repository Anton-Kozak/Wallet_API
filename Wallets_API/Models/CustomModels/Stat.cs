using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.DTO;

namespace Wallets_API.Models.CustomModels
{
    public class Stat
    {
        public double Sum { get; set; }
        public int Usage { get; set; }
        public string CategoryTitle { get; set; }
        public double LargestExpense { get; set; }
    }
}
