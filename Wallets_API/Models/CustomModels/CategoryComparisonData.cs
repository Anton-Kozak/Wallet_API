using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.DBClasses;

namespace Wallets_API.Models.CustomModels
{
    public class CategoryComparisonData
    {
        public double CurrentMonthData { get; set; }
        public double LastMonthData { get; set; }
    }
}
