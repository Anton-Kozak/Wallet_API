using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.Data;
using Wallets_API.DBClasses;
using Wallets_API.DTO;
using Wallets_API.Models.CustomModels;

namespace Wallets_API.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _context;
        public ExpenseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BarExpensesDTO> CreateBarExpensesData(int walletId)
        {
            //TODO: проверить что будет если везде будет 0
            var foodData = await _context.Expenses.Where(e => e.ExpenseCategoryId == 1 && e.FamilyWalletId == walletId).SumAsync(e => e.MoneySpent);
            var houseData = await _context.Expenses.Where(e => e.ExpenseCategoryId == 2 && e.FamilyWalletId == walletId).SumAsync(e => e.MoneySpent);
            var clothesData = await _context.Expenses.Where(e => e.ExpenseCategoryId == 3 && e.FamilyWalletId == walletId).SumAsync(e => e.MoneySpent);
            var enterData = await _context.Expenses.Where(e => e.ExpenseCategoryId == 4 && e.FamilyWalletId == walletId).SumAsync(e => e.MoneySpent);
            var otherData = await _context.Expenses.Where(e => e.ExpenseCategoryId == 5 && e.FamilyWalletId == walletId).SumAsync(e => e.MoneySpent);
            BarExpensesDTO barExpenses = new BarExpensesDTO
            {
                HouseExpenses = houseData,
                FoodExpenses = foodData,
                EntertainmentExpenses = enterData,
                ClothesExpenses = clothesData,
                OtherExpenses = otherData
            };
            return barExpenses;
        }

        public async Task<Expense> CreateNewExpense(Expense newExpense)
        {
            _context.Expenses.Add(newExpense);
            if (await _context.SaveChangesAsync() > 0)
                return newExpense;
            return null;
        }

        public async Task<DetailedUserStatisticsDTO> DetailedUserStatistics(int walletId)
        {
            DetailedUserStatisticsDTO data = new DetailedUserStatisticsDTO();
            //получить категории в которых больше трат и использований
            int categoryIdForSum, categoryIdForUsage;
            double largestExpense;
            GetTopCategories(walletId, out categoryIdForSum, out categoryIdForUsage, out largestExpense);

            switch (categoryIdForSum)
            {
                case 1:
                    data.MostSpentCategory = "Food";
                    break;
                case 2:
                    data.MostSpentCategory = "Housekeeping";
                    break;
                case 3:
                    data.MostSpentCategory = "Clothes";
                    break;
                case 4:
                    data.MostSpentCategory = "Entertainment";
                    break;
                case 5:
                    data.MostSpentCategory = "Other";
                    break;
                default:
                    data.MostSpentCategory = "Nothing";
                    break;
            }
            switch (categoryIdForUsage)
            {
                case 1:
                    data.MostUsedCategory = "Food";
                    break;
                case 2:
                    data.MostUsedCategory = "Housekeeping";
                    break;
                case 3:
                    data.MostUsedCategory = "Clothes";
                    break;
                case 4:
                    data.MostUsedCategory = "Entertainment";
                    break;
                case 5:
                    data.MostUsedCategory = "Other";
                    break;
                default:
                    data.MostUsedCategory = "Nothing";
                    break;
            }
            //средние показатели расходов
            data.AverageDailyExpense = Math.Round(await _context.Expenses.Where(e => e.FamilyWalletId == walletId).AverageAsync(e => e.MoneySpent), 2);


            //общие показатели по всем категориям за всё время
            //TODO: потом поменять на данный месяц?
            data.BarExpenses = await CreateBarExpensesData(walletId);

            //получить данные предыдущего и текущего месяцев для сравнения по всем категориям всех пользователей данного кошелька
            data.BarCompareExpensesWithLastMonth = await GetCurrentAndPreviousMonthsData(walletId);

            //данные о 5 пользователях с наибольшим кол-вом трат в кошельке
            data.TopFiveUsers = await GetTopMembers(walletId);

            data.LastSixMonths = await GetLastSixMonthsOfData(walletId);

            //wallet members
            data.WalletUsers = await _context.Users.Where(u => u.WalletID == walletId).Select(u => u.UserName).ToArrayAsync();

            data.AmountOfMoneySpent = await _context.Expenses.Where(e => e.FamilyWalletId == walletId).SumAsync(s => s.MoneySpent);

            return data;
        }

        private async Task<List<LastMonthData>> GetLastSixMonthsOfData(int walletId)
        {
            CultureInfo ci = new CultureInfo("en-US");
            List<LastMonthData> lastMonths = new List<LastMonthData>();
            var today = DateTime.Today;
            var currentMonth = new DateTime(today.Year, today.Month, 1);
            var monthToRemove = new DateTime(today.Year, today.Month, 1);
            DateTime startOfPreviousMonth;
            DateTime endOfPreviousMonth;
            for (int i = -1; i > -6; i--)
            {
                startOfPreviousMonth = currentMonth.AddMonths(i);
                endOfPreviousMonth = monthToRemove.AddMilliseconds(-1);
                monthToRemove = monthToRemove.AddMonths(-1);

                lastMonths.Add(new LastMonthData
                {
                    Month = DateTime.Now.AddMonths(i).ToString("MMMM", ci),
                    ExpenseSum = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.CreationDate >= startOfPreviousMonth && e.CreationDate <= endOfPreviousMonth).SumAsync(e => e.MoneySpent),
                });
            }
            return lastMonths;
        }

        private async Task<List<UserStatistics>> GetTopMembers(int walletId)
        {

            var topFiveMembers = await (from e in _context.Expenses
                      join u in _context.Users on e.ExpenseUserId equals u.Id
                      where e.FamilyWalletId == walletId
                      group e by u.UserName into g
                      select new UserStatistics
                      {
                          Sum = g.Sum(s => s.MoneySpent),
                          UserName = g.Key
                      }).OrderByDescending(e => e.Sum).Take(5).ToListAsync();

            //var topFiveMembers = await _context.Expenses.Where(e => e.FamilyWalletId == walletId).GroupBy(e => e.ExpenseUserId).Select(res => new UserStatistics
            //{
            //    Sum = res.Sum(s => s.MoneySpent),
            //    UserName = res.First()
            //}).OrderByDescending(e => e.Sum).Take(5).ToListAsync();
            return topFiveMembers;
        }

        private async Task<BarComparison> GetCurrentAndPreviousMonthsData(int walletId)
        {
            var today = DateTime.Today;
            var currentMonth = new DateTime(today.Year, today.Month, 1);
            var startOfPreviousMonth = currentMonth.AddMonths(-1);
            var endOfPreviousMonth = currentMonth.AddMilliseconds(-1);
            var endOfCurrentMonth = currentMonth.AddMonths(1).AddMilliseconds(-1);



            var lastMonthData = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.CreationDate >= startOfPreviousMonth && e.CreationDate <= endOfPreviousMonth).ToListAsync();
            var currentMonthData = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.CreationDate >= currentMonth && e.CreationDate <= endOfCurrentMonth).ToListAsync();

            var lastFoodData = lastMonthData.Where(d => d.ExpenseCategoryId == 1).Sum(e => e.MoneySpent);
            var lastHouseData = lastMonthData.Where(d => d.ExpenseCategoryId == 2).Sum(e => e.MoneySpent);
            var lastClothesData = lastMonthData.Where(d => d.ExpenseCategoryId == 3).Sum(e => e.MoneySpent);
            var lastEntData = lastMonthData.Where(d => d.ExpenseCategoryId == 4).Sum(e => e.MoneySpent);
            var lastOtherData = lastMonthData.Where(d => d.ExpenseCategoryId == 5).Sum(e => e.MoneySpent);

            BarExpensesDTO lastBarExpenses = new BarExpensesDTO
            {
                HouseExpenses = lastHouseData,
                FoodExpenses = lastFoodData,
                EntertainmentExpenses = lastEntData,
                ClothesExpenses = lastClothesData,
                OtherExpenses = lastOtherData
            };

            var currentFoodData = currentMonthData.Where(d => d.ExpenseCategoryId == 1).Sum(e => e.MoneySpent);
            var currentHouseData = currentMonthData.Where(d => d.ExpenseCategoryId == 2).Sum(e => e.MoneySpent);
            var currentClothesData = currentMonthData.Where(d => d.ExpenseCategoryId == 3).Sum(e => e.MoneySpent);
            var currentEntData = currentMonthData.Where(d => d.ExpenseCategoryId == 4).Sum(e => e.MoneySpent);
            var currentOtherData = currentMonthData.Where(d => d.ExpenseCategoryId == 5).Sum(e => e.MoneySpent);

            BarExpensesDTO currentbarExpenses = new BarExpensesDTO
            {
                HouseExpenses = currentHouseData,
                FoodExpenses = currentFoodData,
                EntertainmentExpenses = currentEntData,
                ClothesExpenses = currentClothesData,
                OtherExpenses = currentOtherData
            };

            BarComparison barComparison = new BarComparison
            {
                CurrentMonthData = currentbarExpenses,
                LastMonthData = lastBarExpenses
            };

            return barComparison;
        }

        private void GetTopCategories(int walletId, out int categoryIdForSum, out int categoryIdForUsage, out double largestExpense)
        {
            var sumForCategory = _context.Expenses.Where(e => e.FamilyWalletId == walletId).GroupBy(e => e.ExpenseCategoryId).Select(res => new Statistics
            {
                Sum = res.Sum(s => s.MoneySpent),
                Usage = res.Count(),
                CategoryId = res.First().ExpenseCategoryId,
                LargestExpense = res.Max(e => e.MoneySpent)
            });
            largestExpense = sumForCategory.Max(m => m.LargestExpense);
            categoryIdForSum = sumForCategory.OrderByDescending(e => e.Sum).Select(c => c.CategoryId).First();
            categoryIdForUsage = sumForCategory.OrderByDescending(e => e.Usage).Select(c => c.CategoryId).First();
        }

        public async Task<IEnumerable<Expense>> ShowAllExpenses(int walletId)
        {
            return await _context.Expenses.Where(e => e.FamilyWalletId == walletId).ToListAsync();
        }



    }
}
