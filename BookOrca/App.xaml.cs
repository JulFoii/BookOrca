using System.IO;
using System.Windows;
using BookOrca.ApiAccess;
using BookOrca.Core;
using BookOrca.DataAccess;
using BookOrca.Resources;
using BookOrca.View;
using BookOrca.ViewModel;
using ControlzEx.Theming;

namespace BookOrca;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        Singletons.BookApi = new OpenLibraryService();
        Singletons.BookDataAccess = new BookDataAccess();
        
        // Create important directories

        var foldersToMake = new[] { Paths.BookPath, Paths.MetadataPath, Paths.ImagesPath };

        foreach (var folderToMake in foldersToMake)
            if (!Directory.Exists(folderToMake))
                Directory.CreateDirectory(folderToMake);

        // Load theme
        var config = ConfigurationManager.LoadConfiguration();

        var theme = config["Theme"];
        var color = config["Color"];

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