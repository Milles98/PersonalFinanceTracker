using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using PersonalFinanceTracker.Command;
using PersonalFinanceTracker.Data;
using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.Services;
using PersonalFinanceTracker.Views;

namespace PersonalFinanceTracker.ViewModels
{
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

        public MainViewModel()
        {
            _userService = new UserService();
            
            ShowUserProfileCommand = new RelayCommand(_ => ShowUserProfile());
            ShowLoginViewCommand = new RelayCommand(_ => ShowLoginView());
            ShowRegisterViewCommand = new RelayCommand(_ => ShowRegisterView());
            ShowTransactionEntryViewCommand = new RelayCommand(_ => ShowTransactionEntryView());
            ShowTransactionHistoryViewCommand = new RelayCommand(_ => ShowTransactionHistoryView());
            LogoutCommand = new RelayCommand(_ => Logout());

            // Set initial view
            ShowLoginView();
        }

        private void ShowUserProfile()
        {
            CurrentView = new UserProfileView { DataContext = new UserProfileViewModel(_userService, _currentUserId)};
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
                CurrentView = new TransactionEntryView { DataContext = new TransactionEntryViewModel(_currentUserId) };
            }
        }

        private void ShowTransactionHistoryView()
        {
            if (IsLoggedIn)
            {
                CurrentView = new TransactionHistoryView { DataContext = new TransactionHistoryViewModel(_currentUserId, ShowTransactionUpdateView) };
            }
        }
        
        private void ShowTransactionUpdateView(TransactionUpdateViewModel updateViewModel)
        {
            CurrentView = new TransactionUpdateView { DataContext = updateViewModel };
        }

        // test for commit

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
}
