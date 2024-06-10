using System.Windows;
using PersonalFinanceTracker.ViewModels;

namespace PersonalFinanceTracker;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = ((ViewModelLocator)Application.Current.Resources["Locator"]).MainViewModel;
    }
}