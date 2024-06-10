using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using PersonalFinanceTracker.Command;
using PersonalFinanceTracker.Services;

namespace PersonalFinanceTracker.ViewModels
{
    public class UserProfileViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;
        private string _username;
        private string _currentPassword;
        private string _newPassword;
        private string _confirmPassword;
        private int _userId;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string CurrentPassword
        {
            get { return _currentPassword; }
            set
            {
                _currentPassword = value;
                OnPropertyChanged();
            }
        }

        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                OnPropertyChanged();
            }
        }

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
            }
        }

        public ICommand UpdateProfileCommand { get; }
        public ICommand ChangePasswordCommand { get; }

        public UserProfileViewModel(IUserService userService, int userId)
        {
            _userService = userService;
            _userId = userId;

            UpdateProfileCommand = new RelayCommand(_ => UpdateProfile());
            ChangePasswordCommand = new RelayCommand(_ => ChangePassword());
        }

        private void UpdateProfile()
        {
            if (_userService.VerifyPassword(_userId, _currentPassword))
            {
                var success = _userService.UpdateProfile(_userId, _username);
                if (success)
                {
                    MessageBox.Show("Username updated successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to update username.");
                }
            }
            else
            {
                MessageBox.Show("Incorrect password.");
            }
        }

        private void ChangePassword()
        {
            if (_newPassword != _confirmPassword)
            {
                MessageBox.Show("New passwords do not match.");
                return;
            }

            var success = _userService.ChangePassword(_userId, _newPassword);
            if (success)
            {
                MessageBox.Show("Password changed successfully.");
            }
            else
            {
                MessageBox.Show("Failed to change password.");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
