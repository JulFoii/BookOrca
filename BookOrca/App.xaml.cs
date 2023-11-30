using System.IO;
using System.Windows;
using BookOrca.Resources;
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
            Directory.CreateDirectory(Paths.BookPath);
            Directory.CreateDirectory(Paths.ImagesPath);
            Directory.CreateDirectory(Paths.MetadataPath);
        }
        
        Current.MainWindow = new MainWindow
        {
            DataContext = MainViewModel.Instance
        };

        Current.MainWindow.Show();
        
        base.OnStartup(e);
    }
}