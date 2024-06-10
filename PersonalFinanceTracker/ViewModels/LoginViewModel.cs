using System.ComponentModel;
using System.Windows.Input;
using PersonalFinanceTracker.Data;
using System.Linq;
using PersonalFinanceTracker.Command;
using PersonalFinanceTracker.Services;

namespace PersonalFinanceTracker.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private string _username;
    private string _password;
    private readonly IUserService _userService;
    private readonly Action<string> _showWelcomeViewAction;

    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged(nameof(Username));
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    public ICommand LoginCommand { get; }

    public LoginViewModel(IUserService userService, Action<string> showWelcomeViewAction)
    {
        _userService = userService;
        _showWelcomeViewAction = showWelcomeViewAction;
        LoginCommand = new RelayCommand(Login);
    }

    private void Login(object obj)
    {
        var user = _userService.GetUserByUsernameAndPassword(Username, Password);
        if (user != null)
        {
            _showWelcomeViewAction(user.Username);
        }
        else
        {
            System.Windows.MessageBox.Show("Invalid username or password.");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
