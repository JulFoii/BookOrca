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

        var foldersToMake = new[] { Paths.BookPath, Paths.MetadataPath, Paths.ImagesPath };

        foreach (var folderToMake in foldersToMake)
            if (!Directory.Exists(folderToMake))
                Directory.CreateDirectory(folderToMake);


        // Load theme
        var theme = ConfigurationManager.AppSettings["Theme"] ?? "Light";
        var color = ConfigurationManager.AppSettings["Color"] ?? "Blue";

        ThemeManager.Current.ChangeTheme(this, $"{theme}.{color}");

        Current.MainWindow = new MainWindow
        {
            DataContext = MainViewModel.Instance
        };

        Current.MainWindow.Show();

        MainViewModel.Instance.UpdateBooksCommand.Execute();

        base.OnStartup(e);
    }
}