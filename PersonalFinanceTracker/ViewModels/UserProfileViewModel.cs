using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PersonalFinanceTracker.Command;

namespace PersonalFinanceTracker.ViewModels
{
    public class UserProfileViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public ICommand UpdateProfileCommand { get; }
        public ICommand ChangePasswordCommand { get; }

        public UserProfileViewModel()
        {
            UpdateProfileCommand = new RelayCommand(_ =>UpdateProfile());
            ChangePasswordCommand = new RelayCommand(_ =>ChangePassword());
        }

        private void UpdateProfile()
        {
            // Logic to update profile
        }

        private void ChangePassword()
        {
            // Logic to change password
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