using System.IO;
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
        if (!Directory.Exists("books"))
        {
            Directory.CreateDirectory("books");
        }
        
        Current.MainWindow = new MainWindow
        {
            DataContext = new MainViewModel()
        };

        Current.MainWindow.Show();
        
        base.OnStartup(e);
    }
}