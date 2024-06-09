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
    
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}