using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.Repository;

public interface ITransactionRepository
{
    IEnumerable<Transaction> GetTransactionsByUserId(int userId);
    void AddTransaction(Transaction transaction);
    void UpdateTransaction(Transaction transaction);
    void DeleteTransaction(Transaction transaction);
    IEnumerable<Category> GetCategories();
    void AddCategory(Category category);
}