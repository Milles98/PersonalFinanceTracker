using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.ViewModels;

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

                if (!context.Transactions.Any())
                {
                    context.Transactions.AddRange(
                        new Transaction
                        {
                            Description = "Groceries",
                            Amount = 50,
                            Category = "Food",
                            Date = DateTime.Now,
                            UserId = user.Id 
                        },
                        new Transaction
                        {
                            Description = "Rent",
                            Amount = 1200,
                            Category = "Housing",
                            Date = DateTime.Now,
                            UserId = user.Id 
                        }
                    );
                }
                
                context.SaveChanges();
            }
        }
    }
}