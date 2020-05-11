using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.DTO;

namespace Wallets_API.Models.CustomModels
{
    public class Statistics
    {
        public double Sum { get; set; }
        public int Usage { get; set; }
        public int CategoryId { get; set; }
        public double LargestExpense { get; set; }
    }
}
