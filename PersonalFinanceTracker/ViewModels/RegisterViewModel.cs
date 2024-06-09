using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PersonalFinanceTracker.Command;
using PersonalFinanceTracker.Data;
using PersonalFinanceTracker.Models;

namespace PersonalFinanceTracker.ViewModels;

public class RegisterViewModel : INotifyPropertyChanged
{
    private string _username;
    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
        }
    }

    private string _password;
    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    public ICommand RegisterCommand { get; }

    private readonly Action _showLoginViewAction;

    public RegisterViewModel(Action showLoginViewAction)
    {
        RegisterCommand = new RelayCommand(Register);
        _showLoginViewAction = showLoginViewAction;
    }

    private void Register(object obj)
    {
        using (var context = new FinanceContext())
        {
            var user = new User { Username = Username, Password = Password };
            context.Users.Add(user);
            context.SaveChanges();
            _showLoginViewAction();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}