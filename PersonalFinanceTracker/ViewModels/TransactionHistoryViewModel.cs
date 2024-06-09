using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PersonalFinanceTracker.Command;
using PersonalFinanceTracker.Data;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.ViewModels
{
    public class TransactionHistoryViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Transaction> Transactions { get; set; }
        public ICommand DeleteTransactionCommand { get; }
        public ICommand UpdateTransactionCommand { get; }

        private readonly int _currentUserId;
        private string _successMessage;
        private bool _isSuccessMessageVisible;

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

        public TransactionHistoryViewModel(int currentUserId)
        {
            _currentUserId = currentUserId;
            Transactions = new ObservableCollection<Transaction>();
            DeleteTransactionCommand = new RelayCommand(DeleteTransaction);
            UpdateTransactionCommand = new RelayCommand(UpdateTransaction);
            LoadTransactions();
        }

        private void LoadTransactions()
        {
            using (var context = new FinanceContext())
            {
                var transactions = context.Transactions
                    .Where(u => u.UserId == _currentUserId)
                    .ToList();
                Transactions.Clear();
                foreach (var transaction in transactions)
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
                    ShowSuccessMessage("Transaction deleted successfully!");
                }
            }
        }

        private void UpdateTransaction(object obj)
        {
            if (obj is Transaction transaction)
            {
                using (var context = new FinanceContext())
                {
                    var existingTransactions = context.Transactions.FirstOrDefault(t => t.Id == transaction.Id);
                    if (existingTransactions != null)
                    {
                        existingTransactions.Description = transaction.Description;
                        existingTransactions.Amount = transaction.Amount;
                        existingTransactions.Category = transaction.Category;
                        existingTransactions.Date = transaction.Date;
                        context.SaveChanges();
                        ShowSuccessMessage("Transaction updated successfully!");
                    }
                }
            }
        }

        private async void ShowSuccessMessage(string message)
        {
            SuccessMessage = message;
            IsSuccessMessageVisible = true;
            await Task.Delay(2000);
            IsSuccessMessageVisible = false;
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
