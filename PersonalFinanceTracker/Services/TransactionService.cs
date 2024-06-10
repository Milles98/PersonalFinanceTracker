using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.Repository;

namespace PersonalFinanceTracker.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public IEnumerable<Transaction> GetTransactionsByUserId(int userId)
    {
        return _transactionRepository.GetTransactionsByUserId(userId);
    }

    public void AddTransaction(Transaction transaction)
    {
        _transactionRepository.AddTransaction(transaction);
    }

    public void UpdateTransaction(Transaction transaction)
    {
        _transactionRepository.UpdateTransaction(transaction);
    }

    public void DeleteTransaction(Transaction transaction)
    {
        _transactionRepository.DeleteTransaction(transaction);
    }

    public IEnumerable<Category> GetCategories()
    {
        return _transactionRepository.GetCategories();
    }

    public void AddCategory(Category category)
    {
        _transactionRepository.AddCategory(category);
    }
}