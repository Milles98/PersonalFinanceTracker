﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Threading;
using PersonalFinanceTracker.Command;
using PersonalFinanceTracker.Data;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.ViewModels
{
    public class TransactionUpdateViewModel : INotifyPropertyChanged
    {
        private Transaction _transaction;
        private readonly int _currentUserId;
        private string _successMessage;
        private readonly DispatcherTimer _successMessageTimer;

        public Transaction Transaction
        {
            get => _transaction;
            set
            {
                _transaction = value;
                OnPropertyChanged(nameof(Transaction));
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
        public ICommand UpdateTransactionCommand { get; }
        public Action<Transaction> OnTransactionUpdated { get; set; }

        public TransactionUpdateViewModel(Transaction transaction, int currentUserId,
            Action<Transaction> onTransactionUpdated)
        {
            _transaction = transaction;
            _currentUserId = currentUserId;
            OnTransactionUpdated = onTransactionUpdated;

            Categories = new ObservableCollection<Category>();
            UpdateTransactionCommand = new RelayCommand(UpdateTransaction);

            LoadCategories();

            _successMessageTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
            _successMessageTimer.Tick += (sender, args) =>
            {
                SuccessMessage = string.Empty;
                _successMessageTimer.Stop();
            };
        }

        private void LoadCategories()
        {
            using (var context = new FinanceContext())
            {
                var categories = context.Categories.ToList();
                Categories.Clear();
                foreach (var category in categories)
                {
                    Categories.Add(category);
                }
            }
        }

        private void UpdateTransaction(object obj)
        {
            using (var context = new FinanceContext())
            {
                var existingTransaction = context.Transactions.FirstOrDefault(t => t.Id == Transaction.Id);
                if (existingTransaction != null)
                {
                    existingTransaction.Description = Transaction.Description;
                    existingTransaction.Amount = Transaction.Amount;
                    existingTransaction.CategoryId = Transaction.CategoryId;
                    existingTransaction.Date = Transaction.Date;
                    context.SaveChanges();

                    SuccessMessage = "Transaction was updated successfully!";
                    OnTransactionUpdated?.Invoke(existingTransaction);
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
}