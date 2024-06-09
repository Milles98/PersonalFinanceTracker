using System.Collections.ObjectModel;
using System.ComponentModel;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private ObservableCollection<Transaction> _transactions = null!;
    public ObservableCollection<Transaction> Transactions
    {
        get => _transactions;
        set
        {
            _transactions = value;
            OnPropertyChanged(nameof(Transactions));
        }
    }
    
    public MainViewModel()
    {
        Transactions =
        [
            new Transaction { Description = "Groceries", Amount = 50, Category = "Food", Date = DateTime.Now },
            new Transaction { Description = "Rent", Amount = 1200, Category = "Housing", Date = DateTime.Now }
        ];
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}