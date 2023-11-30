using System.Configuration;
using System.IO;
using System.Windows;
using BookOrca.Resources;
using BookOrca.View;
using BookOrca.ViewModel;
using ControlzEx.Theming;

namespace BookOrca;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        // Create important directories
        if (!Directory.Exists("books"))
        {
            Directory.CreateDirectory(Paths.BookPath);
            Directory.CreateDirectory(Paths.ImagesPath);
            Directory.CreateDirectory(Paths.MetadataPath);
        }
        
        // Load theme
        var theme = ConfigurationManager.AppSettings["Theme"] ?? "Light";
        var color = ConfigurationManager.AppSettings["Color"] ?? "Blue";

        ThemeManager.Current.ChangeTheme(this, $"{theme}.{color}");
        
        Current.MainWindow = new MainWindow
        {
            DataContext = MainViewModel.Instance
        };

        Current.MainWindow.Show();
        
        base.OnStartup(e);
    }
}