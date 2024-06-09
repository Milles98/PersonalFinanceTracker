using System.ComponentModel;
using System.Windows.Input;
using PersonalFinanceTracker.Data;
using System.Linq;
using PersonalFinanceTracker.Command;

namespace PersonalFinanceTracker.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
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

    public ICommand LoginCommand { get; }
    public ICommand ShowRegisterViewCommand { get; }

    private readonly Action _showRegisterViewAction;
    private readonly Action<string> _showWelcomeViewAction;

    public LoginViewModel(Action showRegisterViewAction, Action<string> showWelcomeViewAction)
    {
        LoginCommand = new RelayCommand(Login);
        ShowRegisterViewCommand = new RelayCommand(_ => showRegisterViewAction());

        _showRegisterViewAction = showRegisterViewAction;
        _showWelcomeViewAction = showWelcomeViewAction;
    }

    private void Login(object obj)
    {
        using (var context = new FinanceContext())
        {
            var user = context.FinanceUsers.FirstOrDefault(u => u.Username == Username && u.Password == Password);
            if (user != null)
            {
                _showWelcomeViewAction(user.Username);
            }
            else
            {
                System.Windows.MessageBox.Show("Invalid username or password.");
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}