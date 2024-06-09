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

            if (!context.Users.Any())
            {
                var user = new User
                {
                    Username = "Mille",
                    Password = "123"
                };

                context.Users.Add(user);
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
                            UserId = user.Id // Use the generated UserId
                        },
                        new Transaction
                        {
                            Description = "Rent",
                            Amount = 1200,
                            Category = "Housing",
                            Date = DateTime.Now,
                            UserId = user.Id // Use the generated UserId
                        }
                    );
                }
                
                context.SaveChanges();
            }
        }
    }
}