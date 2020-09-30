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

        public async Task<List<CategoriesAndExpensesDTO>> CreateBarExpensesData(int walletId, DateTime d)
        {
            DateTime[] date = GetDate(d);
            var monthStart = date[0];
            var monthEnd = date[1];
            //TODO: проверить что будет если везде будет 0
            List<CategoriesAndExpensesDTO> expenses = new List<CategoriesAndExpensesDTO>();
            foreach (var category in _context.WalletsCategories.Where(w => w.WalletId == walletId).AsEnumerable())
            {
                CategoriesAndExpensesDTO categoriesAndExpenses = new CategoriesAndExpensesDTO();
                categoriesAndExpenses.CategoryExpenses = await _context.Expenses.Where(e => e.ExpenseCategoryId == category.CategoryId && e.FamilyWalletId == walletId && e.CreationDate >= monthStart && e.CreationDate <= monthEnd).SumAsync(e => e.MoneySpent);
                expenses.Add(categoriesAndExpenses);
            }
            return expenses;
        }

        public async Task<Expense> CreateNewExpense(Expense newExpense)
        {
            _context.Expenses.Add(newExpense);
            if (await _context.SaveChangesAsync() > 0)
                return newExpense;
            return null;
        }

        public async Task<PreviousExpenses> ShowPreviousExpenses(int walletId, DateTime d)
        {
            DateTime[] date = GetDate(d);
            var currentMonth = date[0];
            var endOfCurrentMonth = date[1];
            //var currentMonth = new DateTime(date.Year, date.Month, 1);
            //var endOfCurrentMonth = currentMonth.AddMonths(1).AddMilliseconds(-1);
            var categories = await _walletRepo.GetCategories(walletId);
            PreviousExpenses previousExpenses = new PreviousExpenses();
            List<ExpensesWithCategoryData> expenses = new List<ExpensesWithCategoryData>();
            foreach (var category in categories)
            {
                ExpensesWithCategoryData expGroup = new ExpensesWithCategoryData();
                expGroup.CategoryId = category.Id;
                expGroup.CategoryName = category.Title;

                var catExpenses = await (from e in _context.Expenses
                                         join u in _context.Users
                                         on e.ExpenseUserId equals u.Id
                                         where e.ExpenseCategoryId == category.Id
                                         where e.FamilyWalletId == walletId
                                         where e.CreationDate >= currentMonth && e.CreationDate <= endOfCurrentMonth
                                         select new ExpenseDTO
                                         {
                                             Id = e.Id,
                                             UserName = u.UserName,
                                             ExpenseTitle = e.ExpenseTitle,
                                             ExpenseDescription = e.ExpenseDescription,
                                             CreationDate = e.CreationDate,
                                             MoneySpent = e.MoneySpent,
                                         }).Take(10).ToListAsync();
                if (catExpenses.Count == 0)
                    expGroup.Expenses = new List<ExpenseDTO>();
                else
                    expGroup.Expenses = catExpenses;
                expenses.Add(expGroup);
            }
            previousExpenses.PreviousMonthExpenses = expenses;
            previousExpenses.TopFiveUsers = await GetTopMembers(walletId, currentMonth, endOfCurrentMonth);
            previousExpenses.PreviousExpensesBars = await CreateBarExpensesData(walletId, d);
            return previousExpenses;
        }

        public async Task<List<ExpensesWithCategoryData>> ShowExpenses(int walletId)
        {
            var today = DateTime.Today;
            var currentMonth = new DateTime(today.Year, today.Month, 1);
            var endOfCurrentMonth = currentMonth.AddMonths(1).AddMilliseconds(-1);
            List<ExpensesWithCategoryData> expenses = new List<ExpensesWithCategoryData>();

            var categories = await _walletRepo.GetCategories(walletId);
            foreach (var category in categories)
            {
                ExpensesWithCategoryData expGroup = new ExpensesWithCategoryData();
                expGroup.CategoryId = category.Id;
                expGroup.CategoryName = category.Title;

                var catExpenses = await (from e in _context.Expenses
                                         join u in _context.Users
                                         on e.ExpenseUserId equals u.Id
                                         where e.ExpenseCategoryId == category.Id
                                         where e.FamilyWalletId == walletId
                                         where e.CreationDate >= currentMonth && e.CreationDate <= endOfCurrentMonth
                                         select new ExpenseDTO
                                         {
                                             Id = e.Id,
                                             UserName = u.UserName,
                                             ExpenseTitle = e.ExpenseTitle,
                                             ExpenseDescription = e.ExpenseDescription,
                                             CreationDate = e.CreationDate,
                                             MoneySpent = e.MoneySpent,
                                         }).Take(10).ToListAsync();
                if (catExpenses.Count == 0)
                    expGroup.Expenses = new List<ExpenseDTO>();
                else
                    expGroup.Expenses = catExpenses;
                expenses.Add(expGroup);
            }
            return expenses;
        }

        public async Task<List<ExpenseDTO>> ShowDailyExpenses(int walletId, DateTime date)
        {
            var currentDay = date.Date;
            var endOfCurrentDay = date.Date.AddDays(1).AddTicks(-1);
            var expenses = await (from e in _context.Expenses
                                  join u in _context.Users
                                  on e.ExpenseUserId equals u.Id
                                  join c in _context.ExpenseCategories
                                  on e.ExpenseCategoryId equals c.Id
                                  where e.FamilyWalletId == walletId
                                  && e.CreationDate >= currentDay
                                  && e.CreationDate <= endOfCurrentDay
                                  select new ExpenseDTO
                                  {
                                      Id = e.Id,
                                      UserName = u.UserName,
                                      ExpenseTitle = e.ExpenseTitle,
                                      ExpenseDescription = e.ExpenseDescription,
                                      CreationDate = e.CreationDate,
                                      MoneySpent = e.MoneySpent,
                                      ExpenseCategory = c.Title
                                  }).ToListAsync();
            var reversedExpenses = expenses.Reverse<ExpenseDTO>().ToList();
            return reversedExpenses;
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

        public async Task<List<ExpenseDTO>> ShowCategoryExpenses(int walletId, int categoryId, DateTime[] date)
        {
            if (walletId != 0 && categoryId != 0)
            {
                var monthStart = date[0];
                var monthEnd = date[1];
                var expenses = await (from e in _context.Expenses
                                      join u in _context.Users
                                      on e.ExpenseUserId equals u.Id
                                      where e.FamilyWalletId == walletId && e.ExpenseCategoryId == categoryId && e.CreationDate >= monthStart && e.CreationDate <= monthEnd
                                      select new ExpenseDTO
                                      {
                                          UserName = u.UserName,
                                          ExpenseTitle = e.ExpenseTitle,
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

        public async Task<Expense> EditExpense(string userId, ExpenseDTO expenseToEdit)
        {
            var expToEdit = await _context.Expenses.Where(e => e.Id == expenseToEdit.Id && e.ExpenseUserId == userId).FirstOrDefaultAsync();

            if (expToEdit != null)
            {
                expToEdit.ExpenseTitle = expenseToEdit.ExpenseTitle;
                expToEdit.ExpenseDescription = expenseToEdit.ExpenseDescription;
                expToEdit.MoneySpent = expenseToEdit.MoneySpent;
                expToEdit.CreationDate = expenseToEdit.CreationDate;
                _context.Expenses.Update(expToEdit);
                await _context.SaveChangesAsync();
                return expToEdit;
            }
            return null;
        }

        //-----------------------------------------------wallet statistics-------------------------------------------------------------

        public async Task<DetailedWalletStatisticsDTO> DetailedWalletStatistics(int walletId, DateTime d)
        {
            DetailedWalletStatisticsDTO data = new DetailedWalletStatisticsDTO();
            data.hasExpenseData = false;
            //получить категории в которых больше трат и использований
            int categoryIdForSum, categoryIdForUsage;
            double largestExpense;
            GetWalletTopCategories(walletId, d, out categoryIdForSum, out categoryIdForUsage, out largestExpense);
            if (largestExpense > 0)
            {
                DateTime[] date = GetDate(d);
                var monthStart = date[0];
                var monthEnd = date[1];

                data.hasExpenseData = true;
                data.MostSpentCategory = await _context.ExpenseCategories.Where(e => e.Id == categoryIdForSum).Select(e => e.Title).FirstOrDefaultAsync();
                data.MostUsedCategory = await _context.ExpenseCategories.Where(e => e.Id == categoryIdForUsage).Select(e => e.Title).FirstOrDefaultAsync();

                //средние показатели расходов
                data.AverageDailyExpense = Math.Round(await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.CreationDate >= monthStart && e.CreationDate <= monthEnd).AverageAsync(e => e.MoneySpent), 2);


                //общие показатели по всем категориям за всё время
                //TODO: потом поменять на данный месяц?
                data.BarExpenses = await CreateBarExpensesData(walletId, d);

                //получить данные предыдущего и текущего месяцев для сравнения по всем категориям всех пользователей данного кошелька
                data.BarCompareExpensesWithLastMonth = await GetCurrentAndPreviousMonthsData(walletId);

                //данные о 5 пользователях с наибольшим кол-вом трат в кошельке
                data.TopFiveUsers = await GetTopMembers(walletId, monthStart, monthEnd);

                data.LastSixMonths = await GetLastSixMonthsOfData(walletId);

                data.AmountOfMoneySpent = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.CreationDate >= monthStart && e.CreationDate <= monthEnd).SumAsync(s => s.MoneySpent);
            }
            //wallet members
            //var users = await _context.Users.Where(u => u.WalletID == walletId).ToListAsync();
            //var users = await (from u in _context.Users
            //                   join p in _context.Photos
            //                   on u.UserPhotoId equals p.Id
            //                   where u.WalletID == walletId
            //                   select new UserForDisplayDTO
            //                   {
            //                       Id = u.Id,
            //                       Address = u.Address,
            //                       Age = u.Age,
            //                       DateJoined = u.DateJoined,
            //                       Username = u.UserName,
            //                       WalletID = u.WalletID,
            //                       PhotoUrl = p.Url
            //                   }).ToArrayAsync();

            var users = await (from u in _context.Users
                               where u.WalletID == walletId
                               select new UserForDisplayDTO
                               {
                                   Id = u.Id,
                                   Address = u.Address,
                                   Age = u.Age,
                                   DateJoined = u.DateJoined,
                                   Username = u.UserName,
                                   WalletID = u.WalletID,
                                   PhotoUrl = null
                               }).ToArrayAsync();

            var userPhotos = await (from p in _context.Photos
                                    join u in _context.Users
                                    on p.Id equals u.UserPhotoId
                                    join us in users
                                    on u.Id equals us.Id
                                    where p.Id == u.UserPhotoId
                                    where u.WalletID == walletId
                                    select new UserPhoto
                                    {
                                        UserId = u.Id,
                                        PhotoUrl = p.Url
                                    }).ToArrayAsync();

            for (int i = 0; i < users.Length; i++)
            {
                for (int k = 0; k < userPhotos.Length; k++)
                {
                    if (users[i].Id == userPhotos[k].UserId)
                    {
                        users[i].PhotoUrl = userPhotos[k].PhotoUrl;
                        break;
                    }
                }
               
            }
            data.WalletUsers = users;
            return data;
        }

        private async Task<List<LastMonthData>> GetLastSixMonthsOfData(int walletId)
        {
            CultureInfo ci = new CultureInfo("en-US");
            List<LastMonthData> lastMonths = new List<LastMonthData>();
            var today = DateTime.Today;
            var currentMonth = new DateTime(today.Year, today.Month, 1);
            var monthToRemove = new DateTime(today.Year, today.Month, 1);
            lastMonths.Add(new LastMonthData
            {
                Month = DateTime.Now.AddMonths(0).ToString("MMMM", ci),
                ExpenseSum = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.CreationDate >= currentMonth && e.CreationDate <= currentMonth.AddMonths(1).AddMilliseconds(-1)).SumAsync(e => e.MoneySpent),
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
                    ExpenseSum = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.CreationDate >= startOfPreviousMonth && e.CreationDate <= endOfPreviousMonth).SumAsync(e => e.MoneySpent),
                });
            }
            return lastMonths;
        }

        private async Task<List<UserStatistics>> GetTopMembers(int walletId, DateTime start, DateTime end)
        {
            var monthStart = start;
            var monthEnd = end;
            var topFiveMembers = await (from e in _context.Expenses
                                        join u in _context.Users on e.ExpenseUserId equals u.Id
                                        where e.FamilyWalletId == walletId
                                        where e.CreationDate >= monthStart && e.CreationDate <= monthEnd
                                        group e by u.UserName into g
                                        select new UserStatistics
                                        {
                                            Sum = g.Sum(s => s.MoneySpent),
                                            UserName = g.Key
                                        }).OrderByDescending(e => e.Sum).Take(5).ToListAsync();
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

            List<CategoriesAndExpensesDTO> currentMonthExpenses = new List<CategoriesAndExpensesDTO>();
            List<CategoriesAndExpensesDTO> lastMonthExpenses = new List<CategoriesAndExpensesDTO>();
            foreach (var category in _context.WalletsCategories.Where(w => w.WalletId == walletId).AsEnumerable())
            {
                CategoriesAndExpensesDTO currentMonthcategoriesAndExpenses = new CategoriesAndExpensesDTO();
                //sum of current month expenses for this wallet and this category
                var currentMonthCategoryExpense = _context.Expenses.Where(e => e.ExpenseCategoryId == category.CategoryId && e.FamilyWalletId == walletId && e.CreationDate >= currentMonth && e.CreationDate <= endOfCurrentMonth).Sum(e => e.MoneySpent);
                currentMonthcategoriesAndExpenses.Id = category.CategoryId;
                currentMonthcategoriesAndExpenses.CategoryExpenses = currentMonthCategoryExpense;

                CategoriesAndExpensesDTO lastMonthcategoriesAndExpenses = new CategoriesAndExpensesDTO();
                //sum of last month expenses for this wallet and this category
                var lastMonthCategoryExpenses = _context.Expenses.Where(e => e.ExpenseCategoryId == category.CategoryId && e.FamilyWalletId == walletId && e.CreationDate >= startOfPreviousMonth && e.CreationDate <= endOfPreviousMonth).Sum(e => e.MoneySpent);
                lastMonthcategoriesAndExpenses.Id = category.CategoryId;
                lastMonthcategoriesAndExpenses.CategoryExpenses = lastMonthCategoryExpenses;

                currentMonthExpenses.Add(currentMonthcategoriesAndExpenses);
                lastMonthExpenses.Add(lastMonthcategoriesAndExpenses);
            }

            BarComparison barComparison = new BarComparison
            {
                CurrentMonthData = currentMonthExpenses,
                LastMonthData = lastMonthExpenses
            };

            return barComparison;
        }

        private void GetWalletTopCategories(int walletId, DateTime d, out int categoryIdForSum, out int categoryIdForUsage, out double largestExpense)
        {
            DateTime[] date = GetDate(d);
            var monthStart = date[0];
            var monthEnd = date[1];
            var sumForCategory = _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.CreationDate >= monthStart && e.CreationDate <= monthEnd).GroupBy(e => e.ExpenseCategoryId).Select(res => new Statistics
            {
                Sum = res.Sum(s => s.MoneySpent),
                Usage = res.Count(),
                CategoryId = res.First().ExpenseCategoryId,
                LargestExpense = res.Max(e => e.MoneySpent)
            });
            if (sumForCategory.ToList().Count != 0)
            {
                largestExpense = sumForCategory.Max(m => m.LargestExpense);
                categoryIdForSum = sumForCategory.OrderByDescending(e => e.Sum).Select(c => c.CategoryId).First();
                categoryIdForUsage = sumForCategory.OrderByDescending(e => e.Usage).Select(c => c.CategoryId).First();
            }
            else
            {
                largestExpense = 0;
                categoryIdForSum = 0;
                categoryIdForUsage = 0;
            }
        }

        public async Task<SpecifiedMonthsData> ShowSpecifiedMonthsData(int walletId, DateTime firstMonth, DateTime secondMonth)
        {
            DateTime[] firstDate = GetDate(firstMonth);
            DateTime[] secondDate = GetDate(secondMonth);
            SpecifiedMonthsData data = new SpecifiedMonthsData();
            //FIRST MONTH
            data.FirstMonthTopFiveUsers = await GetTopMembers(walletId, firstDate[0], firstDate[1]);
            int firstCategoryIdForSum, firstCategoryIdForUsage;
            double firstLargestExpense;
            GetWalletTopCategories(walletId, firstMonth, out firstCategoryIdForSum, out firstCategoryIdForUsage, out firstLargestExpense);
            data.FirstMonthAverage = Math.Round(await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.CreationDate >= firstDate[0] && e.CreationDate <= firstDate[1]).AverageAsync(e => e.MoneySpent), 2);
            data.FirstMonthMostSpent = await _context.ExpenseCategories.Where(e => e.Id == firstCategoryIdForSum).Select(e => e.Title).FirstOrDefaultAsync();
            data.FirstMonthMostUsed = await _context.ExpenseCategories.Where(e => e.Id == firstCategoryIdForUsage).Select(e => e.Title).FirstOrDefaultAsync();
            data.FirstMonthTotal = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.CreationDate >= firstDate[0] && e.CreationDate <= firstDate[1]).SumAsync(s => s.MoneySpent);
            data.FirstMonthPreviousExpensesBars = await CreateBarExpensesData(walletId, firstMonth);
            data.FirstLargestExpense = firstLargestExpense;
            data.FirstMonthExpenses = await GetExpensesForSpecifiedMonthComparison(firstMonth, walletId);

            //SECOND MONTH
            data.SecondMonthTopFiveUsers = await GetTopMembers(walletId, secondDate[0], secondDate[1]);
            int secondCategoryIdForSum, secondCategoryIdForUsage;
            double secondLargestExpense;
            GetWalletTopCategories(walletId, secondMonth, out secondCategoryIdForSum, out secondCategoryIdForUsage, out secondLargestExpense);
            data.SecondMonthAverage = Math.Round(await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.CreationDate >= secondDate[0] && e.CreationDate <= secondDate[1]).AverageAsync(e => e.MoneySpent), 2);
            data.SecondMonthMostSpent = await _context.ExpenseCategories.Where(e => e.Id == secondCategoryIdForSum).Select(e => e.Title).FirstOrDefaultAsync();
            data.SecondMonthMostUsed = await _context.ExpenseCategories.Where(e => e.Id == secondCategoryIdForUsage).Select(e => e.Title).FirstOrDefaultAsync();
            data.SecondMonthTotal = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.CreationDate >= secondDate[0] && e.CreationDate <= secondDate[1]).SumAsync(s => s.MoneySpent);
            data.SecondMonthPreviousExpensesBars = await CreateBarExpensesData(walletId, secondMonth);
            data.SecondLargestExpense = secondLargestExpense;
            data.SecondMonthExpenses = await GetExpensesForSpecifiedMonthComparison(secondMonth, walletId);
            return data;
        }

        private async Task<List<ExpenseDTO>> GetExpensesForSpecifiedMonthComparison(DateTime selectedDay, int walletId)
        {
            var expenses = await (from e in _context.Expenses
                                  join u in _context.Users
                                  on e.ExpenseUserId equals u.Id
                                  join c in _context.ExpenseCategories
                                  on e.ExpenseCategoryId equals c.Id
                                  where e.FamilyWalletId == walletId && e.CreationDate >= selectedDay.Date && e.CreationDate <= selectedDay.Date.AddDays(1).AddTicks(-1)
                                  select new ExpenseDTO
                                  {
                                      Id = e.Id,
                                      ExpenseTitle = e.ExpenseTitle,
                                      CreationDate = e.CreationDate,
                                      MoneySpent = e.MoneySpent,
                                      ExpenseCategory = c.Title,
                                      UserName = u.UserName
                                  }).ToListAsync();
            return expenses;

        }

        //-----------------------------------------------category statistics-------------------------------------------------------------


        public async Task<DetailedCategoryStatisticsDTO> DetailedCategoryStatistics(int walletId, int categoryId, string userId, DateTime d)
        {
            DetailedCategoryStatisticsDTO data = new DetailedCategoryStatisticsDTO();
            DateTime[] date = GetDate(d);
            var monthStart = date[0];
            var monthEnd = date[1];
            //TODO: подумать о том, чтобы объеденить методы для пользователя и категории в одни и те же, просто сделать if для запроса с катгорией и без для запроса к пользователю
            //убрать дубляж кода
            var expenses = await ShowCategoryExpenses(walletId, categoryId, date);
            if (expenses.Count != 0)
                data.LargestExpense = expenses.AsQueryable().Max(e => e.MoneySpent);
            else
                data.LargestExpense = 0;
            data.CategoryExpenses = expenses;
            data.CurrentMonthLargestExpense = await GetCurrentMonthLargestExpense(walletId, categoryId, date);
            data.MostSpentUser = await GetMostSpentUser(walletId, categoryId, date);
            data.MostUsedUser = await GetMostUsedUser(walletId, categoryId, date);
            data.BarCompareExpensesWithLastMonth = await GetCurrentAndPreviousMonthsDataForCategory(walletId, categoryId);
            data.TopFiveUsers = await GetTopMembersForCategory(walletId, categoryId, date);
            data.LastSixMonths = await GetLastSixMonthsOfDataForCategory(walletId, categoryId);
            data.SpentThisMonth = await GetCurrentMonthExpenses(walletId, categoryId, userId, date);
            data.SpentAll = await _context.Expenses.Where(e => e.FamilyWalletId == walletId
                                                        && e.ExpenseCategoryId == categoryId
                                                        && e.ExpenseUserId == userId
                                                        && e.CreationDate >= monthStart
                                                        && e.CreationDate <= monthEnd).SumAsync(e => e.MoneySpent);

            return data;
        }

        private async Task<List<UserStatistics>> GetTopMembersForCategory(int walletId, int categoryId, DateTime[] date)
        {
            var monthStart = date[0];
            var monthEnd = date[1];
            var topFiveMembers = await (from e in _context.Expenses
                                        join u in _context.Users on e.ExpenseUserId equals u.Id
                                        where e.FamilyWalletId == walletId && e.ExpenseCategoryId == categoryId && e.CreationDate >= monthStart && e.CreationDate <= monthEnd
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

        private async Task<double> GetCurrentMonthLargestExpense(int walletId, int categoryId, DateTime[] date)
        {
            var monthStart = date[0];
            var monthEnd = date[1];
            var expenses = await _context.Expenses.Where(e => e.FamilyWalletId == walletId
                                                        && e.ExpenseCategoryId == categoryId
                                                        && e.CreationDate >= monthStart
                                                        && e.CreationDate <= monthEnd)
                                                        .ToListAsync();
            if (expenses.Count != 0)
                return expenses.AsQueryable().Max(e => e.MoneySpent);
            else
                return 0;
        }

        private async Task<double> GetCurrentMonthExpenses(int walletId, int categoryId, string userId, DateTime[] date)
        {
            var monthStart = date[0];
            var monthEnd = date[1];

            return await _context.Expenses.Where(e => e.FamilyWalletId == walletId
                                                        && e.ExpenseCategoryId == categoryId
                                                        && e.CreationDate >= monthStart
                                                        && e.CreationDate <= monthEnd
                                                        && e.ExpenseUserId == userId)
                                                        .SumAsync(e => e.MoneySpent);
        }

        private async Task<UserStatistics> GetMostUsedUser(int walletId, int categoryId, DateTime[] date)
        {
            var monthStart = date[0];
            var monthEnd = date[1];
            var user = await (from e in _context.Expenses
                              join u in _context.Users on e.ExpenseUserId equals u.Id
                              where e.FamilyWalletId == walletId && e.ExpenseCategoryId == categoryId && e.CreationDate >= monthStart && e.CreationDate <= monthEnd
                              group e by u.UserName into g
                              select new UserStatistics
                              {
                                  Sum = Convert.ToDouble(g.Count()),
                                  UserName = g.Key
                              }).OrderByDescending(e => e.Sum).Take(1).FirstOrDefaultAsync();
            return user;

        }

        private async Task<UserStatistics> GetMostSpentUser(int walletId, int categoryId, DateTime[] date)
        {
            var monthStart = date[0];
            var monthEnd = date[1];
            var mostSpentUser = await (from e in _context.Expenses
                                       join u in _context.Users on e.ExpenseUserId equals u.Id
                                       where e.FamilyWalletId == walletId && e.ExpenseCategoryId == categoryId && e.CreationDate >= monthStart && e.CreationDate <= monthEnd
                                       group e by u.UserName into g
                                       select new UserStatistics
                                       {
                                           Sum = g.Sum(s => s.MoneySpent),
                                           UserName = g.Key
                                       }).OrderByDescending(e => e.Sum).Take(1).FirstOrDefaultAsync();
            return mostSpentUser;
        }

        private async Task<MonthsComparisonData> GetCurrentAndPreviousMonthsDataForCategory(int walletId, int categoryId)
        {
            var today = DateTime.Today;
            var currentMonth = new DateTime(today.Year, today.Month, 1);
            var startOfPreviousMonth = currentMonth.AddMonths(-1);
            var endOfPreviousMonth = currentMonth.AddMilliseconds(-1);
            var endOfCurrentMonth = currentMonth.AddMonths(1).AddMilliseconds(-1);



            var lastMonthData = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.ExpenseCategoryId == categoryId && e.CreationDate >= startOfPreviousMonth && e.CreationDate <= endOfPreviousMonth).SumAsync(e => e.MoneySpent);
            var currentMonthData = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.ExpenseCategoryId == categoryId && e.CreationDate >= currentMonth && e.CreationDate <= endOfCurrentMonth).SumAsync(e => e.MoneySpent);

            MonthsComparisonData data = new MonthsComparisonData
            {
                CurrentMonthData = currentMonthData,
                LastMonthData = lastMonthData,
            };

            return data;
        }




        //----------------------------------------------user statistics----------------------------------------------------------------

        public async Task<DetailedUserStatisticsDTO> DetailedUserStatistics(int walletId, string userId, DateTime d)
        {
            DetailedUserStatisticsDTO data = new DetailedUserStatisticsDTO();
            DateTime[] date = GetDate(d);
            var monthStart = date[0];
            var monthEnd = date[1];
            int categoryIdForSum, categoryIdForUsage;
            double largestExpense;
            GetUserTopCategories(walletId, userId, date, out categoryIdForSum, out categoryIdForUsage, out largestExpense);
            if (largestExpense > 0)
            {
                data.MonthCompareData = await GetCurrentAndPreviousMonthsDataForUser(walletId, userId, date);
                data.LastSixMonths = await GetLastSixMonthsOfDataForUser(walletId, userId, date);
                data.LargestExpense = largestExpense;
                data.MostSpentCategory = await _context.ExpenseCategories.Where(e => e.Id == categoryIdForSum).Select(e => e.Title).FirstOrDefaultAsync();
                data.MostUsedCategory = await _context.ExpenseCategories.Where(e => e.Id == categoryIdForUsage).Select(e => e.Title).FirstOrDefaultAsync();
                var avgExp = _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.ExpenseUserId == userId && e.CreationDate >= monthStart && e.CreationDate <= monthEnd);
                if (avgExp.Count() != 0)
                    data.AverageDailyExpense = Math.Round(await avgExp.AverageAsync(e => e.MoneySpent), 2);
                else
                    data.AverageDailyExpense = 0;
                data.BarExpenses = await CreateBarExpensesDataForUser(walletId, userId, date);
                data.AmountOfMoneySpent = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.ExpenseUserId == userId && e.CreationDate >= monthStart && e.CreationDate <= monthEnd).SumAsync(s => s.MoneySpent);
            }
            return data;

        }

        private void GetUserTopCategories(int walletId, string userId, DateTime[] date, out int categoryIdForSum, out int categoryIdForUsage, out double largestExpense)
        {
            var monthStart = date[0];
            var monthEnd = date[1];
            var sumForCategory = _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.CreationDate >= monthStart && e.CreationDate <= monthEnd && e.ExpenseUserId == userId).GroupBy(e => e.ExpenseCategoryId).Select(res => new Statistics
            {
                Sum = res.Sum(s => s.MoneySpent),
                Usage = res.Count(),
                CategoryId = res.First().ExpenseCategoryId,
                LargestExpense = res.Max(e => e.MoneySpent)
            });
            if (sumForCategory.ToList().Count != 0)
            {
                largestExpense = sumForCategory.Max(m => m.LargestExpense);
                categoryIdForSum = sumForCategory.OrderByDescending(e => e.Sum).Select(c => c.CategoryId).First();
                categoryIdForUsage = sumForCategory.OrderByDescending(e => e.Usage).Select(c => c.CategoryId).First();
            }
            else
            {
                largestExpense = 0;
                categoryIdForSum = 0;
                categoryIdForUsage = 0;
            }
        }

        private async Task<List<CategoriesAndExpensesDTO>> CreateBarExpensesDataForUser(int walletId, string userId, DateTime[] date)
        {
            var monthStart = date[0];
            var monthEnd = date[1];
            List<CategoriesAndExpensesDTO> expenses = new List<CategoriesAndExpensesDTO>();
            foreach (var category in _context.WalletsCategories.Where(w => w.WalletId == walletId).AsEnumerable())
            {
                CategoriesAndExpensesDTO categoriesAndExpenses = new CategoriesAndExpensesDTO();
                categoriesAndExpenses.CategoryExpenses = _context.Expenses.Where(e => e.ExpenseCategoryId == category.CategoryId && e.FamilyWalletId == walletId && e.ExpenseUserId == userId && e.CreationDate >= monthStart && e.CreationDate <= monthEnd).Sum(e => e.MoneySpent);
                expenses.Add(categoriesAndExpenses);
            }

            return expenses;
        }

        private async Task<MonthsComparisonData> GetCurrentAndPreviousMonthsDataForUser(int walletId, string userId, DateTime[] date)
        {
            var currentMonthStart = date[0];
            var currentMonthEnd = date[1];

            var previousMonthStart = date[0].AddMonths(-1);
            var previousMonthEnd = date[1].AddMonths(-1);

            var lastMonthData = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.ExpenseUserId == userId && e.CreationDate >= previousMonthStart && e.CreationDate <= previousMonthEnd).SumAsync(e => e.MoneySpent);
            var currentMonthData = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.ExpenseUserId == userId && e.CreationDate >= currentMonthStart && e.CreationDate <= currentMonthEnd).SumAsync(e => e.MoneySpent);

            MonthsComparisonData data = new MonthsComparisonData
            {
                CurrentMonthData = currentMonthData,
                LastMonthData = lastMonthData,
            };

            return data;
        }

        private async Task<List<LastMonthData>> GetLastSixMonthsOfDataForUser(int walletId, string userId, DateTime[] date)
        {
            CultureInfo ci = new CultureInfo("en-US");
            List<LastMonthData> lastMonths = new List<LastMonthData>();
            var currentMonth = date[0];
            var monthToRemove = date[0];
            lastMonths.Add(new LastMonthData
            {
                Month = DateTime.Now.AddMonths(0).ToString("MMMM", ci),
                ExpenseSum = await _context.Expenses.Where(e => e.FamilyWalletId == walletId && e.ExpenseUserId == userId && e.CreationDate >= currentMonth && e.CreationDate <= date[1]).SumAsync(e => e.MoneySpent),
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


        public async Task<List<ExpenseDTO>> ShowUserExpenses(int walletId, string userId, DateTime d)
        {
            if (walletId != 0 && userId != null)
            {
                DateTime[] date = GetDate(d);
                var monthStart = date[0];
                var monthEnd = date[1];
                var expenses = await (from e in _context.Expenses
                                      join u in _context.Users
                                      on e.ExpenseUserId equals u.Id
                                      join c in _context.ExpenseCategories
                                      on e.ExpenseCategoryId equals c.Id
                                      where e.FamilyWalletId == walletId && e.ExpenseUserId == userId && e.CreationDate >= monthStart && e.CreationDate <= monthEnd
                                      select new ExpenseDTO
                                      {
                                          Id = e.Id,
                                          ExpenseTitle = e.ExpenseTitle,
                                          ExpenseDescription = e.ExpenseDescription,
                                          CreationDate = e.CreationDate,
                                          MoneySpent = e.MoneySpent,
                                          ExpenseCategory = c.Title
                                      }).ToListAsync();
                return expenses;
            }
            return null;
        }

        //private DateTime[] GetDate(int month)
        //{
        //    DateTime[] date = new DateTime[2];
        //    var today = DateTime.Today;
        //    var previousDate = today.AddMonths(month);
        //    date[0] = new DateTime(previousDate.Year, previousDate.Month, 1);
        //    date[1] = date[0].AddMonths(1).AddMilliseconds(-1);
        //    return date;
        //}


        private DateTime[] GetDate(DateTime d)
        {
            DateTime[] date = new DateTime[2];
            date[0] = new DateTime(d.Year, d.Month, 1);
            date[1] = date[0].AddMonths(1).AddMilliseconds(-1);
            return date;
        }

    }
}
