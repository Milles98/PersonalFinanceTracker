using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PersonalFinanceTracker.Command;
using PersonalFinanceTracker.Data;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.ViewModels
{
    public class TransactionUpdateViewModel : INotifyPropertyChanged
    {
        private Transaction _transaction;

        public Transaction Transaction
        {
            get => _transaction;
            set
            {
                _transaction = value;
                OnPropertyChanged(nameof(Transaction));
            }
        }

        public ICommand UpdateTransactionCommand { get; }

        public TransactionUpdateViewModel(Transaction transaction)
        {
            Transaction = transaction;
            UpdateTransactionCommand = new RelayCommand(UpdateTransaction);
        }

        private void UpdateTransaction(object obj)
        {
            using (var context = new FinanceContext())
            {
                var existingTransaction = context.Transactions.Find(Transaction.Id);
                if (existingTransaction != null)
                {
                    existingTransaction.Description = Transaction.Description;
                    existingTransaction.Amount = Transaction.Amount;
                    existingTransaction.Category = Transaction.Category;
                    existingTransaction.Date = Transaction.Date;
                    context.SaveChanges();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}