using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PersonalFinanceTracker.Command;
using PersonalFinanceTracker.Data;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.ViewModels;

public class TransactionEntryViewModel : INotifyPropertyChanged
{
    private Transaction _newTransaction;
    private readonly int _currentUserId;

    public Transaction NewTransaction
    {
        get => _newTransaction;
        set
        {
            _newTransaction = value;
            OnPropertyChanged(nameof(NewTransaction));
        }
    }
    
    public ObservableCollection<string> Categories { get; set; }
    public ICommand AddTransactionCommand { get; }

    public TransactionEntryViewModel(int currentUserId)
    {
        _currentUserId = currentUserId;
        NewTransaction = new Transaction();
        Categories = new ObservableCollection<string> { "Food", "Housing", "Utilities", "Transportation", "Health", "Insurance", "Entertainment", "Other" };
        AddTransactionCommand = new RelayCommand(AddTransaction);
    }

    private void AddTransaction(object obj)
    {
        using (var context = new FinanceContext())
        {
            NewTransaction.UserId = _currentUserId;
            context.Transactions.Add(NewTransaction);
            context.SaveChanges();
            NewTransaction = new Transaction();
            OnPropertyChanged(nameof(NewTransaction));
        }
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}