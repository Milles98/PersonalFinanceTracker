﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using PersonalFinanceTracker.Command;
using PersonalFinanceTracker.Data;
using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.Services;
using PersonalFinanceTracker.ViewModels;
using PersonalFinanceTracker.Views;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly IUserService _userService;
    private ObservableCollection<Transaction> _transactions;

    public ObservableCollection<Transaction> Transactions
    {
        get => _transactions;
        set
        {
            _transactions = value;
            OnPropertyChanged(nameof(Transactions));
            OnPropertyChanged(nameof(RemainingBalance));
        }
    }

    public decimal RemainingBalance
    {
        get
        {
            var currentDate = DateTime.Now;
            var totalIncome = IncomeEntries?.Where(ie => ie.Date <= currentDate).Sum(ie => ie.Amount) ?? 0;
            var totalExpenses = Transactions?.Sum(t => t.Amount) ?? 0;
            return totalIncome - totalExpenses;
        }
    }

    private ObservableCollection<IncomeEntry> _incomeEntries;

    public ObservableCollection<IncomeEntry> IncomeEntries
    {
        get => _incomeEntries;
        set
        {
            _incomeEntries = value;
            OnPropertyChanged(nameof(IncomeEntries));
            OnPropertyChanged(nameof(RemainingBalance));
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

    private bool _isLoggedIn;

    public bool IsLoggedIn
    {
        get => _isLoggedIn;
        set
        {
            _isLoggedIn = value;
            OnPropertyChanged(nameof(IsLoggedIn));
        }
    }

    private string _currentUsername;

    public string CurrentUsername
    {
        get => _currentUsername;
        set
        {
            _currentUsername = value;
            OnPropertyChanged(nameof(CurrentUsername));
        }
    }

    private int _currentUserId;
    public ICommand ShowLoginViewCommand { get; }
    public ICommand ShowRegisterViewCommand { get; }
    public ICommand LogoutCommand { get; }
    public ICommand ShowTransactionEntryViewCommand { get; }
    public ICommand ShowTransactionHistoryViewCommand { get; }
    public ICommand ShowUserProfileCommand { get; }
    public ICommand ShowIncomeEntryViewCommand { get; }

    public MainViewModel()
    {
        _userService = new UserService();

        ShowUserProfileCommand = new RelayCommand(_ => ShowUserProfile());
        ShowLoginViewCommand = new RelayCommand(_ => ShowLoginView());
        ShowRegisterViewCommand = new RelayCommand(_ => ShowRegisterView());
        ShowTransactionEntryViewCommand = new RelayCommand(_ => ShowTransactionEntryView());
        ShowTransactionHistoryViewCommand = new RelayCommand(_ => ShowTransactionHistoryView());
        ShowIncomeEntryViewCommand = new RelayCommand(_ => ShowIncomeEntryView());
        LogoutCommand = new RelayCommand(_ => Logout());

        // Set initial view
        ShowLoginView();
    }

    private void ReloadData()
    {
        using (var context = new FinanceContext())
        {
            var currentDate = DateTime.Now;
            Transactions =
                new ObservableCollection<Transaction>(context.Transactions
                    .Where(t => t.UserId == _currentUserId)
                    .ToList());
            IncomeEntries =
                new ObservableCollection<IncomeEntry>(context.IncomeEntries
                    .Where(ie => ie.UserId == _currentUserId && ie.Date <= currentDate)
                    .ToList());
        }
    }

    private void ShowIncomeEntryView()
    {
        if (IsLoggedIn)
        {
            CurrentView = new IncomeEntryView { DataContext = new IncomeEntryViewModel(_currentUserId, ReloadData) };
        }
    }

    private void ShowUserProfile()
    {
        CurrentView = new UserProfileView { DataContext = new UserProfileViewModel(_userService, _currentUserId) };
    }

    private void ShowLoginView()
    {
        IsLoggedIn = false;
        CurrentView = new LoginView { DataContext = new LoginViewModel(ShowRegisterView, ShowWelcomeView) };
    }

    private void ShowRegisterView()
    {
        CurrentView = new RegisterView { DataContext = new RegisterViewModel(ShowLoginView) };
    }

    private void ShowTransactionEntryView()
    {
        if (IsLoggedIn)
        {
            CurrentView = new TransactionEntryView { DataContext = new TransactionEntryViewModel(_currentUserId, ReloadData) };
        }
    }

    private void ShowTransactionHistoryView()
    {
        if (IsLoggedIn)
        {
            CurrentView = new TransactionHistoryView
                { DataContext = new TransactionHistoryViewModel(_currentUserId, ShowTransactionUpdateView) };
        }
    }

    private void ShowTransactionUpdateView(TransactionUpdateViewModel updateViewModel)
    {
        CurrentView = new TransactionUpdateView { DataContext = updateViewModel };
    }

    private void ShowWelcomeView(string username)
    {
        using (var context = new FinanceContext())
        {
            var user = context.FinanceUsers.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                _currentUserId = user.Id;
                CurrentUsername = username;
                IsLoggedIn = true;
                ReloadData();
                CurrentView = new WelcomeView { DataContext = new WelcomeViewModel(username) };
            }
        }
    }

    private void Logout()
    {
        IsLoggedIn = false;
        _currentUserId = 0;
        ShowLoginView();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}