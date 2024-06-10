using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Data;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Repository;

public class TransactionRepository : ITransactionRepository
{
    public IEnumerable<Transaction> GetTransactionsByUserId(int userId)
    {
        using (var context = new FinanceContext())
        {
            return context.Transactions
                .Where(t => t.UserId == userId)
                .Include(t => t.Category)
                .ToList();
        }
    }

    public void AddTransaction(Transaction transaction)
    {
        using (var context = new FinanceContext())
        {
            context.Transactions.Add(transaction);
            context.SaveChanges();
        }
    }

    public void UpdateTransaction(Transaction transaction)
    {
        using (var context = new FinanceContext())
        {
            context.Transactions.Update(transaction);
            context.SaveChanges();
        }
    }

    public void DeleteTransaction(Transaction transaction)
    {
        using (var context = new FinanceContext())
        {
            context.Transactions.Remove(transaction);
            context.SaveChanges();
        }
    }

    public IEnumerable<Category> GetCategories()
    {
        using (var context = new FinanceContext())
        {
            return context.Categories.ToList();
        }
    }

    public void AddCategory(Category category)
    {
        using (var context = new FinanceContext())
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }
    }
}