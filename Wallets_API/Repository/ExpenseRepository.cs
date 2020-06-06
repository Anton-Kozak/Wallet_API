using AutoMapper;
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
        private readonly IWalletRepository _walletRepo;
        private readonly IMapper _mapper;

        public ExpenseRepository(ApplicationDbContext context, IMapper mapper, IWalletRepository walletRepo)
        {
            _context = context;
            _mapper = mapper;
            _walletRepo = walletRepo;
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

        public async Task<List<Expense>> ShowCurrentExpenses(int walletId)
        {
            var today = DateTime.Today;
            var currentMonth = new DateTime(today.Year, today.Month, 1);
            var endOfCurrentMonth = currentMonth.AddMonths(1).AddMilliseconds(-1);
            List<Expense> listOfExpensesToReturn = new List<Expense>();

            var categories = await _walletRepo.GetCategories(walletId);

            foreach (var category in categories)
            {
                listOfExpensesToReturn.AddRange(await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.ExpenseCategoryId == category.CategoryId && e.CreationDate >= currentMonth && e.CreationDate <= endOfCurrentMonth).Take(10).ToListAsync());
            }
            return listOfExpensesToReturn;
        }

        public async Task<List<Expense>> ShowPreviousExpenses(int walletId)
        {
            var today = DateTime.Today;
            var currentMonth = new DateTime(today.Year, today.Month, 1);
            var endOfCurrentMonth = currentMonth.AddMonths(1).AddMilliseconds(-1);
            List<Expense> listOfExpensesToReturn = new List<Expense>();

            foreach (var category in _context.ExpenseCategories)
            {
                listOfExpensesToReturn.AddRange(await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.ExpenseCategoryId == category.Id && e.CreationDate < currentMonth).Take(10).ToListAsync());
            }
            return listOfExpensesToReturn;
        }

        public async Task<WalletToReturnDTO> GetWalletData(int walletId)
        {
            var today = DateTime.Today;
            var currentMonth = new DateTime(today.Year, today.Month, 1);
            var endOfCurrentMonth = currentMonth.AddMonths(1).AddMilliseconds(-1);

            var walletTitle = await _context.Wallets.Where(w => w.Id == walletId).Select(w => w.Title).FirstOrDefaultAsync();
            var walletLimit = await _context.Wallets.Where(w => w.Id == walletId).Select(w => w.MonthlyLimit).FirstOrDefaultAsync();
            var monthlyExpenses = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.CreationDate >= currentMonth && e.CreationDate <= endOfCurrentMonth).SumAsync(m => m.MoneySpent);

            var categoryIDs = await _context.WalletsCategories.Where(wc => wc.WalletId == walletId).Select(wc => wc.CategoryId).ToListAsync();
                             
            
            return new WalletToReturnDTO
            {
                Title = walletTitle,
                MonthlyLimit = walletLimit,
                MonthlyExpenses = monthlyExpenses,
                WalletCategories = categoryIDs
            };
        }

        public async Task<List<ExpenseDTO>> ShowCategoryExpenses(int walletId, int categoryId)
        {
            if (walletId != 0 && categoryId != 0)
            {
                var expenses = await (from e in _context.Expenses
                                      join u in _context.Users
                                      on e.ExpenseUserId equals u.Id
                                      where e.FamilyWalletId == walletId && e.ExpenseCategoryId == categoryId
                                      select new ExpenseDTO
                                      {
                                          UserName = u.UserName,
                                          ExpenseTitle = e.ExpenseName,
                                          ExpenseDescription = e.ExpenseDescription,
                                          CreationDate = e.CreationDate,
                                          MoneySpent = e.MoneySpent
                                      }).ToListAsync();
                return expenses;
            }
            return null;
        }


        public async Task<ResponseData> DeleteExpense(string userId, int expenseId)
        {
            ResponseData data = new ResponseData
            {
                isSuccessful = false,
                Message = "",
            };
            var expenseToDelete = await _context.Expenses.Where(e => e.Id == expenseId && e.ExpenseUserId == userId).FirstOrDefaultAsync();

            if (expenseToDelete != null)
            {
                _context.Expenses.Remove(expenseToDelete);
                await _context.SaveChangesAsync();
                data.isSuccessful = true;
                data.Message = "Expense has been successfully deleted";
                return data;
            }
            data.Message = "Expense has not been found";
            return data;
        }

        public async Task<ResponseData> EditExpense(string userId, ExpenseDTO expenseToEdit)
        {
            ResponseData data = new ResponseData
            {
                isSuccessful = false,
                Message = "",
            };
            var expToEdit = await _context.Expenses.Where(e => e.Id == expenseToEdit.Id && e.ExpenseUserId == userId).FirstOrDefaultAsync();

            if (expToEdit != null)
            {
                expToEdit.ExpenseName = expenseToEdit.ExpenseTitle;
                expToEdit.ExpenseDescription = expenseToEdit.ExpenseDescription;
                expToEdit.MoneySpent = expenseToEdit.MoneySpent;
                expToEdit.CreationDate = expenseToEdit.CreationDate;
                _context.Expenses.Update(expToEdit);
                await _context.SaveChangesAsync();
                data.isSuccessful = true;
                data.Message = "Expense has been successfully edited";
                return data;
            }
            data.Message = "Expense has not been found";
            return data;
        }

        //-----------------------------------------------wallet statistics-------------------------------------------------------------

        public async Task<DetailedWalletStatisticsDTO> DetailedWalletStatistics(int walletId)
        {
            DetailedWalletStatisticsDTO data = new DetailedWalletStatisticsDTO();
            //получить категории в которых больше трат и использований
            int categoryIdForSum, categoryIdForUsage;
            double largestExpense;
            GetWalletTopCategories(walletId, out categoryIdForSum, out categoryIdForUsage, out largestExpense);

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
            var users = await _context.Users.Where(u => u.WalletID == walletId).ToListAsync();
            var usersToReturn = _mapper.Map<UserForDisplayDTO[]>(users);
            data.WalletUsers = usersToReturn;

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

        private void GetWalletTopCategories(int walletId, out int categoryIdForSum, out int categoryIdForUsage, out double largestExpense)
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


        //-----------------------------------------------category statistics-------------------------------------------------------------


        public async Task<DetailedCategoryStatisticsDTO> DetailedCategoryStatistics(int walletId, int categoryId, string userId)
        {
            DetailedCategoryStatisticsDTO data = new DetailedCategoryStatisticsDTO();
            //TODO: подумать о том, чтобы объеденить методы для пользователя и категории в одни и те же, просто сделать if для запроса с катгорией и без для запроса к пользователю
            //убрать дубляж кода
            data.LargestExpense = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.ExpenseCategoryId == categoryId).MaxAsync(e => e.MoneySpent);
            data.CurrentMonthLargestExpense = await GetCurrentMonthLargestExpense(walletId, categoryId);
            data.MostSpentUser = await GetMostSpentUser(walletId, categoryId);
            data.MostUsedUser = await GetMostUsedUser(walletId, categoryId);
            data.BarCompareExpensesWithLastMonth = await GetCurrentAndPreviousMonthsDataForCategory(walletId, categoryId);
            data.TopFiveUsers = await GetTopMembersForCategory(walletId, categoryId);
            data.LastSixMonths = await GetLastSixMonthsOfDataForCategory(walletId, categoryId);
            data.SpentThisMonth = await GetCurrentMonthExpenses(walletId, categoryId, userId);
            data.SpentAll = await _context.Expenses.Where(e => e.FamilyWalletId == walletId
                                                        && e.ExpenseCategoryId == categoryId
                                                        && e.ExpenseUserId == userId).SumAsync(e => e.MoneySpent);
            return data;
        }

        private async Task<List<UserStatistics>> GetTopMembersForCategory(int walletId, int categoryId)
        {

            var topFiveMembers = await (from e in _context.Expenses
                                        join u in _context.Users on e.ExpenseUserId equals u.Id
                                        where e.FamilyWalletId == walletId && e.ExpenseCategoryId == categoryId
                                        group e by u.UserName into g
                                        select new UserStatistics
                                        {
                                            Sum = g.Sum(s => s.MoneySpent),
                                            UserName = g.Key
                                        }).OrderByDescending(e => e.Sum).Take(5).ToListAsync();
            return topFiveMembers;
        }

        private async Task<List<LastMonthData>> GetLastSixMonthsOfDataForCategory(int walletId, int categoryId)
        {
            CultureInfo ci = new CultureInfo("en-US");
            List<LastMonthData> lastMonths = new List<LastMonthData>();
            var today = DateTime.Today;
            var currentMonth = new DateTime(today.Year, today.Month, 1);
            var monthToRemove = new DateTime(today.Year, today.Month, 1);
            lastMonths.Add(new LastMonthData
            {
                Month = DateTime.Now.AddMonths(0).ToString("MMMM", ci),
                ExpenseSum = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.ExpenseCategoryId == categoryId && e.CreationDate >= currentMonth && e.CreationDate <= currentMonth.AddMonths(1).AddMilliseconds(-1)).SumAsync(e => e.MoneySpent),
            });
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
                    ExpenseSum = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.ExpenseCategoryId == categoryId && e.CreationDate >= startOfPreviousMonth && e.CreationDate <= endOfPreviousMonth).SumAsync(e => e.MoneySpent),
                });
            }
            return lastMonths;
        }

        private async Task<double> GetCurrentMonthLargestExpense(int walletId, int categoryId)
        {
            var today = DateTime.Today;
            var currentMonth = new DateTime(today.Year, today.Month, 1);
            var endOfCurrentMonth = currentMonth.AddMonths(1).AddMilliseconds(-1);

            return await _context.Expenses.Where(e => e.FamilyWalletId == walletId
                                                        && e.ExpenseCategoryId == categoryId
                                                        && e.CreationDate >= currentMonth
                                                        && e.CreationDate <= endOfCurrentMonth)
                                                        .MaxAsync(e => e.MoneySpent);
        }

        private async Task<double> GetCurrentMonthExpenses(int walletId, int categoryId, string userId)
        {
            var today = DateTime.Today;
            var currentMonth = new DateTime(today.Year, today.Month, 1);
            var endOfCurrentMonth = currentMonth.AddMonths(1).AddMilliseconds(-1);

            return await _context.Expenses.Where(e => e.FamilyWalletId == walletId
                                                        && e.ExpenseCategoryId == categoryId
                                                        && e.CreationDate >= currentMonth
                                                        && e.CreationDate <= endOfCurrentMonth
                                                        && e.ExpenseUserId == userId)
                                                        .SumAsync(e => e.MoneySpent);
        }

        private async Task<UserStatistics> GetMostUsedUser(int walletId, int categoryId)
        {
            var user = await (from e in _context.Expenses
                              join u in _context.Users on e.ExpenseUserId equals u.Id
                              where e.FamilyWalletId == walletId && e.ExpenseCategoryId == categoryId
                              group e by u.UserName into g
                              select new UserStatistics
                              {
                                  Sum = Convert.ToDouble(g.Count()),
                                  UserName = g.Key
                              }).OrderByDescending(e => e.Sum).Take(1).FirstOrDefaultAsync();
            return user;

        }

        private async Task<UserStatistics> GetMostSpentUser(int walletId, int categoryId)
        {
            var mostSpentUser = await (from e in _context.Expenses
                                       join u in _context.Users on e.ExpenseUserId equals u.Id
                                       where e.FamilyWalletId == walletId && e.ExpenseCategoryId == categoryId
                                       group e by u.UserName into g
                                       select new UserStatistics
                                       {
                                           Sum = g.Sum(s => s.MoneySpent),
                                           UserName = g.Key
                                       }).OrderByDescending(e => e.Sum).Take(1).FirstOrDefaultAsync();
            return mostSpentUser;
        }

        private async Task<CategoryComparisonData> GetCurrentAndPreviousMonthsDataForCategory(int walletId, int categoryId)
        {
            var today = DateTime.Today;
            var currentMonth = new DateTime(today.Year, today.Month, 1);
            var startOfPreviousMonth = currentMonth.AddMonths(-1);
            var endOfPreviousMonth = currentMonth.AddMilliseconds(-1);
            var endOfCurrentMonth = currentMonth.AddMonths(1).AddMilliseconds(-1);



            var lastMonthData = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.ExpenseCategoryId == categoryId && e.CreationDate >= startOfPreviousMonth && e.CreationDate <= endOfPreviousMonth).SumAsync(e => e.MoneySpent);
            var currentMonthData = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.ExpenseCategoryId == categoryId && e.CreationDate >= currentMonth && e.CreationDate <= endOfCurrentMonth).SumAsync(e => e.MoneySpent);

            CategoryComparisonData data = new CategoryComparisonData
            {
                CurrentMonthData = currentMonthData,
                LastMonthData = lastMonthData,
            };

            return data;
        }




        //----------------------------------------------user statistics----------------------------------------------------------------

        public async Task<DetailedUserStatisticsDTO> DetailedUserStatistics(int walletId, string userId)
        {

            DetailedUserStatisticsDTO data = new DetailedUserStatisticsDTO();
            int categoryIdForSum, categoryIdForUsage;
            double largestExpense;
            GetUserTopCategories(walletId, userId, out categoryIdForSum, out categoryIdForUsage, out largestExpense);

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

            data.AverageDailyExpense = Math.Round(await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.ExpenseUserId == userId).AverageAsync(e => e.MoneySpent), 2);

            data.BarExpenses = await CreateBarExpensesDataForUser(walletId, userId);

            data.BarCompareExpensesWithLastMonth = await GetCurrentAndPreviousMonthsDataForUser(walletId, userId);

            data.LastSixMonths = await GetLastSixMonthsOfDataForUser(walletId, userId);

            data.AmountOfMoneySpent = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.ExpenseUserId == userId).SumAsync(s => s.MoneySpent);

            return data;

        }

        private void GetUserTopCategories(int walletId, string userId, out int categoryIdForSum, out int categoryIdForUsage, out double largestExpense)
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

        private async Task<BarExpensesDTO> CreateBarExpensesDataForUser(int walletId, string userId)
        {
            //TODO: проверить что будет если везде будет 0
            var foodData = await _context.Expenses.Where(e => e.ExpenseCategoryId == 1 && e.FamilyWalletId == walletId && e.ExpenseUserId == userId).SumAsync(e => e.MoneySpent);
            var houseData = await _context.Expenses.Where(e => e.ExpenseCategoryId == 2 && e.FamilyWalletId == walletId && e.ExpenseUserId == userId).SumAsync(e => e.MoneySpent);
            var clothesData = await _context.Expenses.Where(e => e.ExpenseCategoryId == 3 && e.FamilyWalletId == walletId && e.ExpenseUserId == userId).SumAsync(e => e.MoneySpent);
            var enterData = await _context.Expenses.Where(e => e.ExpenseCategoryId == 4 && e.FamilyWalletId == walletId && e.ExpenseUserId == userId).SumAsync(e => e.MoneySpent);
            var otherData = await _context.Expenses.Where(e => e.ExpenseCategoryId == 5 && e.FamilyWalletId == walletId && e.ExpenseUserId == userId).SumAsync(e => e.MoneySpent);
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

        private async Task<BarComparison> GetCurrentAndPreviousMonthsDataForUser(int walletId, string userId)
        {
            var today = DateTime.Today;
            var currentMonth = new DateTime(today.Year, today.Month, 1);
            var startOfPreviousMonth = currentMonth.AddMonths(-1);
            var endOfPreviousMonth = currentMonth.AddMilliseconds(-1);
            var endOfCurrentMonth = currentMonth.AddMonths(1).AddMilliseconds(-1);



            var lastMonthData = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.ExpenseUserId == userId && e.CreationDate >= startOfPreviousMonth && e.CreationDate <= endOfPreviousMonth).ToListAsync();
            var currentMonthData = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.ExpenseUserId == userId && e.CreationDate >= currentMonth && e.CreationDate <= endOfCurrentMonth).ToListAsync();

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

        private async Task<List<LastMonthData>> GetLastSixMonthsOfDataForUser(int walletId, string userId)
        {
            CultureInfo ci = new CultureInfo("en-US");
            List<LastMonthData> lastMonths = new List<LastMonthData>();
            var today = DateTime.Today;
            var currentMonth = new DateTime(today.Year, today.Month, 1);
            var monthToRemove = new DateTime(today.Year, today.Month, 1);
            lastMonths.Add(new LastMonthData
            {
                Month = DateTime.Now.AddMonths(0).ToString("MMMM", ci),
                ExpenseSum = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.ExpenseUserId == userId && e.CreationDate >= currentMonth && e.CreationDate <= currentMonth.AddMonths(1).AddMilliseconds(-1)).SumAsync(e => e.MoneySpent),
            });
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
                    ExpenseSum = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.ExpenseUserId == userId && e.CreationDate >= startOfPreviousMonth && e.CreationDate <= endOfPreviousMonth).SumAsync(e => e.MoneySpent),
                });
            }
            return lastMonths;
        }


        public async Task<List<ExpenseDTO>> ShowUserExpenses(int walletId, string userId)
        {
            if (walletId != 0 && userId != null)
            {
                var expenses = await (from e in _context.Expenses
                                      join u in _context.Users
                                      on e.ExpenseUserId equals u.Id
                                      where e.FamilyWalletId == walletId && e.ExpenseUserId == userId
                                      select new ExpenseDTO
                                      {
                                          Id = e.Id,
                                          ExpenseTitle = e.ExpenseName,
                                          ExpenseDescription = e.ExpenseDescription,
                                          CreationDate = e.CreationDate,
                                          MoneySpent = e.MoneySpent
                                      }).ToListAsync();
                return expenses;
            }
            return null;
        }


    }
}
