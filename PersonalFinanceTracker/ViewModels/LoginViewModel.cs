using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Input;
using PersonalFinanceTracker.Command;
using PersonalFinanceTracker.Data;

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
    private readonly Action _showTransactionEntryViewAction;

    public LoginViewModel(Action showRegisterViewAction, Action showTransactionEntryViewAction)
    {
        LoginCommand = new RelayCommand(Login);
        ShowRegisterViewCommand = new RelayCommand(_ => _showRegisterViewAction());

        _showRegisterViewAction = showRegisterViewAction;
        _showTransactionEntryViewAction = showTransactionEntryViewAction;
    }

    private void Login(object obj)
    {
        using (var context = new FinanceContext())
        {
            var user = context.Users.FirstOrDefault(u => u.Username == Username && u.Password == Password);
            if (user != null)
            {
                _showTransactionEntryViewAction();
            }
            else
            {
                MessageBox.Show("Ogiltigt användarnamn eller lösenord");
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