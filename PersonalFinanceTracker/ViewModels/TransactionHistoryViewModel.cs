using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PersonalFinanceTracker.Command;
using PersonalFinanceTracker.Data;
using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.Views;

namespace PersonalFinanceTracker.ViewModels;

public class TransactionHistoryViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Transaction> Transactions { get; set; }
    public ICommand DeleteTransactionCommand { get; }

    public TransactionHistoryViewModel()
    {
        Transactions = new ObservableCollection<Transaction>();
        DeleteTransactionCommand = new RelayCommand(DeleteTransaction);
        LoadTransactions();
    }

    private void LoadTransactions()
    {
        using (var context = new FinanceContext())
        {
            var transactions = context.Transactions.ToList();
            Transactions.Clear();
            foreach (var transaction in Transactions)
            {
                Transactions.Add(transaction);
            }
        }
    }
    
    private void DeleteTransaction(object obj)
    {
        if (obj is Transaction transaction)
        {
            using (var context = new FinanceContext())
            {
                context.Transactions.Remove(transaction);
                context.SaveChanges();
                Transactions.Remove(transaction);
            }
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