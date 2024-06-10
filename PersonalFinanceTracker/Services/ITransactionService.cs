using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Services;

public interface ITransactionService
{
    IEnumerable<Transaction> GetTransactionsByUserId(int userId);
    void AddTransaction(Transaction transaction);
    void UpdateTransaction(Transaction transaction);
    void DeleteTransaction(Transaction transaction);
    IEnumerable<Category> GetCategories();
    void AddCategory(Category category);
}