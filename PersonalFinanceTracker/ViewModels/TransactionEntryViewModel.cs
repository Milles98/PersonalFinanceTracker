using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;
using PersonalFinanceTracker.Command;
using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.Services;

public class TransactionEntryViewModel : INotifyPropertyChanged
{
    private Transaction _newTransaction;
    private string _newCategory;
    private string _successMessage;
    private readonly DispatcherTimer _successMessageTimer;
    private readonly ITransactionService _transactionService;
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

    public string NewCategory
    {
        get => _newCategory;
        set
        {
            _newCategory = value;
            OnPropertyChanged(nameof(NewCategory));
        }
    }
    
    public string SuccessMessage
    {
        get => _successMessage;
        set
        {
            _successMessage = value;
            OnPropertyChanged(nameof(SuccessMessage));
            OnPropertyChanged(nameof(IsSuccessMessageVisible));
            if (!string.IsNullOrEmpty(_successMessage))
            {
                _successMessageTimer.Start();
            }
        }
    }

    public bool IsSuccessMessageVisible => !string.IsNullOrEmpty(SuccessMessage);
    public ObservableCollection<Category> Categories { get; set; }
    public ICommand AddTransactionCommand { get; }
    public ICommand AddCategoryCommand { get; }

    public TransactionEntryViewModel(ITransactionService transactionService, int currentUserId)
    {
        _transactionService = transactionService;
        _currentUserId = currentUserId;
        NewTransaction = new Transaction { Date = DateTime.Now };
        Categories = new ObservableCollection<Category>(_transactionService.GetCategories());
        AddTransactionCommand = new RelayCommand(AddTransaction);
        AddCategoryCommand = new RelayCommand(AddCategory);
        _successMessageTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
        _successMessageTimer.Tick += (sender, args) =>
        {
            SuccessMessage = string.Empty;
            _successMessageTimer.Stop();
        };
    }

    private void AddTransaction(object obj)
    {
        NewTransaction.UserId = _currentUserId;
        
        if (NewTransaction.CategoryId == 0)
        {
            System.Windows.MessageBox.Show("Please select a category.");
            return;
        }

        _transactionService.AddTransaction(NewTransaction);
        NewTransaction = new Transaction { Date = DateTime.Now };
        SuccessMessage = "Transaction added successfully!";
        OnPropertyChanged(nameof(NewTransaction));
    }

    private void AddCategory(object obj)
    {
        if (!string.IsNullOrWhiteSpace(NewCategory) && !Categories.Any(c => c.Name == NewCategory))
        {
            var category = new Category { Name = NewCategory };
            _transactionService.AddCategory(category);
            Categories.Add(category);
            NewCategory = string.Empty;
            SuccessMessage = "Category added successfully!";
            OnPropertyChanged(nameof(NewCategory));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
