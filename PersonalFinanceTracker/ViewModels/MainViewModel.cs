﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using PersonalFinanceTracker.Command;
using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.Views;

namespace PersonalFinanceTracker.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Transaction> _transactions;
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
        public ICommand ShowRegisterViewCommand { get; }
        public ICommand ShowTransactionEntryViewCommand { get; }
        public ICommand ShowTransactionHistoryViewCommand { get; }

        public MainViewModel()
        {
            Transactions = new ObservableCollection<Transaction>
            {
                new Transaction { Description = "Groceries", Amount = 50, Category = "Food", Date = DateTime.Now },
                new Transaction { Description = "Rent", Amount = 1200, Category = "Housing", Date = DateTime.Now }
            };

            ShowLoginViewCommand = new RelayCommand(_ => ShowLoginView());
            ShowRegisterViewCommand = new RelayCommand(_ => ShowRegisterView());
            ShowTransactionEntryViewCommand = new RelayCommand(_ => ShowTransactionEntryView());
            ShowTransactionHistoryViewCommand = new RelayCommand(_ => ShowTransactionHistoryView());

            // Set initial view
            ShowLoginView();
        }

        private void ShowLoginView() => CurrentView = new LoginView { DataContext = new LoginViewModel(ShowRegisterView, ShowWelcomeView) };
        private void ShowRegisterView() => CurrentView = new RegisterView { DataContext = new RegisterViewModel(ShowLoginView) };
        private void ShowTransactionEntryView() => CurrentView = new TransactionEntryView();
        private void ShowTransactionHistoryView() => CurrentView = new TransactionHistoryView();
        private void ShowWelcomeView(string username) => CurrentView = new WelcomeView { DataContext = new WelcomeViewModel(username) };

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
