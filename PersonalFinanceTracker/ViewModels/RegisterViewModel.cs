using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PersonalFinanceTracker.Command;

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

    public RegisterViewModel()
    {
        RegisterCommand = new RelayCommand(Register);
    }
    
    private void Register(object obj)
    {
        // Implement registration logic here
        // For simplicity, assume registration is always successful
        // You can add logic to save the user to a database
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