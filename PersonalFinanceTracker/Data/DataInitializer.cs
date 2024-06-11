using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Data
{
    public static class DataInitializer
    {
        public static void SeedData(FinanceContext context)
        {
            context.Database.Migrate();

            if (!context.FinanceUsers.Any())
            {
                var user = new FinanceUser
                {
                    Username = "Mille",
                    Password = "123"
                };

                context.FinanceUsers.Add(user);
                context.SaveChanges();

                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(
                        new Category { Name = "Food" },
                        new Category { Name = "Housing" },
                        new Category { Name = "Utilities" },
                        new Category { Name = "Transportation" },
                        new Category { Name = "Health" },
                        new Category { Name = "Insurance" },
                        new Category { Name = "Entertainment" },
                        new Category { Name = "Other" }
                    );
                    context.SaveChanges();
                }

                if (!context.Transactions.Any())
                {
                    var foodCategory = context.Categories.First(c => c.Name == "Food");
                    var housingCategory = context.Categories.First(c => c.Name == "Housing");

                    context.Transactions.AddRange(
                        new Transaction
                        {
                            Description = "Groceries",
                            Amount = 50,
                            CategoryId = foodCategory.Id,
                            Date = DateTime.Now,
                            UserId = user.Id 
                        },
                        new Transaction
                        {
                            Description = "Rent",
                            Amount = 1200,
                            CategoryId = housingCategory.Id,
                            Date = DateTime.Now,
                            UserId = user.Id 
                        }
                    );
                    context.SaveChanges();
                }

                if (!context.IncomeExpenseEntries.Any())
                {
                    var foodCategory = context.Categories.First(c => c.Name == "Food");
                    var salaryCategory = context.Categories.First(c => c.Name == "Other");

                    context.IncomeExpenseEntries.AddRange(
                        new IncomeExpenseEntry
                        {
                            Description = "Salary",
                            Amount = 3000,
                            CategoryId = salaryCategory.Id,
                            Date = DateTime.Now,
                            UserId = user.Id,
                            Type = "Income"
                        },
                        new IncomeExpenseEntry
                        {
                            Description = "Dinner at restaurant",
                            Amount = 100,
                            CategoryId = foodCategory.Id,
                            Date = DateTime.Now,
                            UserId = user.Id,
                            Type = "Expense"
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}
