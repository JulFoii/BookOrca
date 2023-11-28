using System.Windows;
using BookOrca.View;
using BookOrca.ViewModel;

namespace BookOrca;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        Current.MainWindow = new MainWindow
        {
            DataContext = new MainViewModel()
        };

        Current.MainWindow.Show();

        base.OnStartup(e);
    }
}