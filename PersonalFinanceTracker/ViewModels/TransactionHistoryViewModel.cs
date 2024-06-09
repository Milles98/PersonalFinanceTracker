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
        public ICommand ShowUpdateTransactionViewCommand { get; }

        private readonly int _currentUserId;
        private string _successMessage;
        private bool _isSuccessMessageVisible;
        private readonly Action<Transaction> _showUpdateTransactionViewAction;

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

        public TransactionHistoryViewModel(int currentUserId, Action<Transaction> showUpdateTransactionViewAction)
        {
            _currentUserId = currentUserId;
            _showUpdateTransactionViewAction = showUpdateTransactionViewAction;
            Transactions = new ObservableCollection<Transaction>();
            DeleteTransactionCommand = new RelayCommand(DeleteTransaction);
            ShowUpdateTransactionViewCommand = new RelayCommand(ShowUpdateTransactionView);
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

        private void ShowUpdateTransactionView(object obj)
        {
            if (obj is Transaction transaction)
            {
                _showUpdateTransactionViewAction(transaction);
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
