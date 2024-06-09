using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using PersonalFinanceTracker.Command;
using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.Views;

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

    private object _currentView;
    public object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged(nameof(CurrentView));
        }
    }
    
    public ICommand ShowLoginViewCommand { get; }
    public ICommand ShowTransactionEntryViewCommand { get; }
    public ICommand ShowTransactionHistoryViewCommand { get; }
    
    public MainViewModel()
    {
        Transactions =
        [
            new Transaction { Description = "Groceries", Amount = 50, Category = "Food", Date = DateTime.Now },
            new Transaction { Description = "Rent", Amount = 1200, Category = "Housing", Date = DateTime.Now }
        ];

        ShowLoginViewCommand = new RelayCommand(ShowLoginView);
        ShowTransactionEntryViewCommand = new RelayCommand(ShowTransactionEntryView);
        ShowTransactionHistoryViewCommand = new RelayCommand(ShowTransactionHistoryView);
    }

    private void ShowLoginView(object obj) => CurrentView = new LoginView();
    private void ShowTransactionEntryView(object obj) => CurrentView = new TransactionEntryView();
    private void ShowTransactionHistoryView(object obj) => CurrentView = new TransactionHistoryView();
    
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}