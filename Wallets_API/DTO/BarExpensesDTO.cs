using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wallets_API.DTO
{
    public class BarExpensesDTO
    {
        public double HouseExpenses { get; set; }
        public double FoodExpenses { get; set; }
        public double ClothesExpenses { get; set; }
        public double EntertainmentExpenses { get; set; }
        public double OtherExpenses { get; set; }
    }
}
