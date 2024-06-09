using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PersonalFinanceTracker.ViewModels;

public class WelcomeViewModel : INotifyPropertyChanged
{
    private string _welcomeMessage;

    public string WelcomeMessage
    {
        get => _welcomeMessage;
        set
        {
            _welcomeMessage = value;
            OnPropertyChanged(nameof(WelcomeMessage));
        }
    }

    public WelcomeViewModel(string username)
    {
        WelcomeMessage = $"Welcome to Finance Tracker, {username}!";
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