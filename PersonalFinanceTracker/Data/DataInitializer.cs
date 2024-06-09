using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Data
{
    public static class DataInitializer
    {
        public static void SeedData(FinanceContext context)
        {
            context.Database.Migrate();

            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    Username = "Mille",
                    Password = "123"
                });
            }

            if (!context.Transactions.Any())
            {
                context.Transactions.AddRange(
                    new Transaction
                    {
                        Description = "Groceries",
                        Amount = 50,
                        Category = "Food",
                        Date = DateTime.Now,
                        UserId = 1 
                    },
                    new Transaction
                    {
                        Description = "Rent",
                        Amount = 1200,
                        Category = "Housing",
                        Date = DateTime.Now,
                        UserId = 1 
                    }
                );
            }

            context.SaveChanges();
        }
    }
}