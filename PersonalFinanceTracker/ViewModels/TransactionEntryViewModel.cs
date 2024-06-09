using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Threading;
using PersonalFinanceTracker.Command;
using PersonalFinanceTracker.Data;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.ViewModels
{
    public class TransactionEntryViewModel : INotifyPropertyChanged
    {
        private Transaction _newTransaction;
        private string _newCategory;
        private string _successMessage;
        private readonly DispatcherTimer _successMessageTimer;
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
        public ObservableCollection<string> Categories { get; set; }
        public ICommand AddTransactionCommand { get; }
        public ICommand AddCategoryCommand { get; }
        

        public TransactionEntryViewModel(int currentUserId)
        {
            _currentUserId = currentUserId;
            NewTransaction = new Transaction { Date = DateTime.Now };
            Categories = new ObservableCollection<string> { "Food", "Housing", "Utilities", "Transportation", "Health", "Insurance", "Entertainment", "Other" };
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
            using (var context = new FinanceContext())
            {
                NewTransaction.UserId = _currentUserId;
                context.Transactions.Add(NewTransaction);
                context.SaveChanges();
                NewTransaction = new Transaction { Date = DateTime.Now }; 
                SuccessMessage = "Transaction added successfully!";
                OnPropertyChanged(nameof(NewTransaction));
            }
        }

        private void AddCategory(object obj)
        {
            if (!string.IsNullOrWhiteSpace(NewCategory) && !Categories.Contains(NewCategory))
            {
                Categories.Add(NewCategory);
                NewCategory = string.Empty;
                SuccessMessage = "Category added successfully!";
                OnPropertyChanged(nameof(NewCategory));
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
}
