using System.Configuration;
using System.Data;
using System.Windows;
using PersonalFinanceTracker.Data;

namespace PersonalFinanceTracker;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        using (var context = new FinanceContext())
        {
            DataInitializer.SeedData(context);
        }
    }
}