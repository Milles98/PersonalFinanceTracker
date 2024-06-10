using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using PersonalFinanceTracker.Command;
using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.Services;
using PersonalFinanceTracker.ViewModels;

public class TransactionHistoryViewModel : INotifyPropertyChanged
{
    private readonly ITransactionService _transactionService;
    private readonly int _currentUserId;
    private string _successMessage;
    private bool _isSuccessMessageVisible;
    private readonly Action<TransactionUpdateViewModel> _showUpdateTransactionViewAction;

    public ObservableCollection<Transaction> Transactions { get; set; }
    public ICommand DeleteTransactionCommand { get; }
    public ICommand ShowUpdateTransactionViewCommand { get; }

    public string SuccessMessage
    {
        get => _successMessage;
        set
        {
            _successMessage = value;
            OnPropertyChanged(nameof(SuccessMessage));
        }
    }

    public bool IsSuccessMessageVisible
    {
        get => _isSuccessMessageVisible;
        set
        {
            _isSuccessMessageVisible = value;
            OnPropertyChanged(nameof(IsSuccessMessageVisible));
        }
    }

    public TransactionHistoryViewModel(ITransactionService transactionService, int currentUserId, Action<TransactionUpdateViewModel> showUpdateTransactionViewAction)
    {
        _transactionService = transactionService;
        _currentUserId = currentUserId;
        _showUpdateTransactionViewAction = showUpdateTransactionViewAction;
        Transactions = new ObservableCollection<Transaction>(_transactionService.GetTransactionsByUserId(_currentUserId));
        DeleteTransactionCommand = new RelayCommand(DeleteTransaction);
        ShowUpdateTransactionViewCommand = new RelayCommand(ShowUpdateTransactionView);
    }

    private void DeleteTransaction(object obj)
    {
        if (obj is Transaction transaction)
        {
            _transactionService.DeleteTransaction(transaction);
            Transactions.Remove(transaction);
            ShowSuccessMessage("Transaction deleted successfully!");
        }
    }

    private void ShowUpdateTransactionView(object obj)
    {
        if (obj is Transaction transaction)
        {
            var updateViewModel = new TransactionUpdateViewModel(transaction, _currentUserId, updatedTransaction =>
            {
                UpdateTransactionInList(updatedTransaction);
            });

            _showUpdateTransactionViewAction(updateViewModel);
        }
    }

    private async void ShowSuccessMessage(string message)
    {
        SuccessMessage = message;
        IsSuccessMessageVisible = true;
        await Task.Delay(2000);
        IsSuccessMessageVisible = false;
    }

    public void UpdateTransactionInList(Transaction updatedTransaction)
    {
        var transaction = Transactions.FirstOrDefault(t => t.Id == updatedTransaction.Id);
        if (transaction != null)
        {
            transaction.Description = updatedTransaction.Description;
            transaction.Amount = updatedTransaction.Amount;
            transaction.CategoryId = updatedTransaction.CategoryId;
            transaction.Category = updatedTransaction.Category;
            transaction.Date = updatedTransaction.Date;
            OnPropertyChanged(nameof(Transactions));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
